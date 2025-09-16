// Decompiled with JetBrains decompiler
// Type: PX.Logging.SelfLog
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog.Debugging;
using System;
using System.IO;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Web.Hosting;

#nullable enable
namespace PX.Logging;

internal static class SelfLog
{
  private const 
  #nullable disable
  string DefaultFileName = "selflog.txt";
  private static SelfLog.Impl _instance = (SelfLog.Impl) new SelfLog.Off();

  internal static void Initialize(IConfiguration serilogConfiguration)
  {
    SelfLog._instance = SelfLog.ConfigureOnce(serilogConfiguration);
  }

  private static SelfLog.Impl ConfigureOnce(IConfiguration settings)
  {
    SelfLog.Mode mode;
    try
    {
      mode = ConfigurationBinder.GetValue<SelfLog.Mode>(settings, "selflog.mode", SelfLog.Mode.Startup);
    }
    catch
    {
      mode = SelfLog.Mode.Startup;
    }
    if (mode == SelfLog.Mode.Off)
      return (SelfLog.Impl) new SelfLog.Off();
    SelfLog.Enable((Action<string>) (s =>
    {
      try
      {
        SelfLog._instance.Write(s);
      }
      catch
      {
      }
    }));
    string path = settings["selflog.path"] ?? settings["selflog"] ?? "selflog.txt";
    return mode != SelfLog.Mode.Startup && mode == SelfLog.Mode.Always ? (SelfLog.Impl) new SelfLog.Always(path) : (SelfLog.Impl) new SelfLog.Startup(path);
  }

  public static IDisposable MarkLoggerStartup() => SelfLog._instance.MarkLoggerStartup();

  public static void WriteLine(string format, object arg0 = null, object arg1 = null, object arg2 = null)
  {
    SelfLog.WriteLine(format, arg0, arg1, arg2);
  }

  /// <summary>This method is intended to use inside exception filters <b>only</b>.</summary>
  /// <returns>Always <c>false</c></returns>
  /// <example><code>catch (Exception e) when(SelfLog.WriteAndRethrow(e, "Exception while something"))</code></example>
  public static bool WriteAndRethrow(Exception ex, string format, object arg0 = null, object arg1 = null)
  {
    SelfLog.WriteLine($"{format}{Environment.NewLine}   {{2}}", arg0, arg1, (object) ex);
    return false;
  }

  public static void SafeWriteExceptionInCaller(
    Exception exception,
    [CallerMemberName] string memberName = "",
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0)
  {
    SelfLog.WriteLine($"Exception in {{0}}{Environment.NewLine}   {{1}}", (object) $"{memberName} ({sourceFilePath}:{sourceLineNumber.ToString()})", (object) exception.ToString());
  }

  private class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterBuildCallback((Action<ILifetimeScope>) (container => container.Disposer.AddInstanceForDisposal(Disposable.Create((Action) (() => Interlocked.Exchange<SelfLog.Impl>(ref SelfLog._instance, (SelfLog.Impl) new SelfLog.Off()).Dispose())))));
    }
  }

  private abstract class Impl : IDisposable
  {
    public abstract void Write(string s);

    public abstract void Dispose();

    public virtual IDisposable MarkLoggerStartup() => Disposable.Empty;

    protected static string GetPath(string path)
    {
      string path1 = HostingEnvironment.MapPath("~/App_Data/") ?? Environment.CurrentDirectory;
      path = path.StartsWith("~/", StringComparison.Ordinal) ? HostingEnvironment.MapPath(path) : Path.Combine(path1, path);
      return SelfLog.AppendOnlyFile.CheckFileAvailability(path, true) ?? SelfLog.AppendOnlyFile.CheckFileAvailability(Path.Combine(path1, "selflog.txt"), false);
    }

    protected static byte[] GetBuffer(string s)
    {
      return Encoding.UTF8.GetBytes(s + Environment.NewLine + Environment.NewLine);
    }
  }

  private class Off : SelfLog.Impl
  {
    public override void Write(string s)
    {
    }

    public override void Dispose()
    {
    }
  }

  private class Always : SelfLog.Impl
  {
    private readonly SelfLog.AppendOnlyFile _appendOnlyFile;

    public Always(string path)
    {
      this._appendOnlyFile = new SelfLog.AppendOnlyFile(SelfLog.Impl.GetPath(path));
    }

    public override void Write(string s) => this._appendOnlyFile.Write(SelfLog.Impl.GetBuffer(s));

    public override void Dispose() => this._appendOnlyFile.Dispose();
  }

  private class Startup : SelfLog.Impl
  {
    private readonly string _path;
    private bool _disposed;
    private readonly object _gate = new object();
    private SelfLog.AppendOnlyFile _appendOnlyFile;
    private int _refCount;

    public Startup(string path) => this._path = SelfLog.Impl.GetPath(path);

    public override void Write(string s) => this._appendOnlyFile?.Write(SelfLog.Impl.GetBuffer(s));

    public override void Dispose()
    {
      if (Volatile.Read(ref this._disposed))
        return;
      this.DisposeImpl();
      Volatile.Write(ref this._disposed, true);
    }

    public override IDisposable MarkLoggerStartup()
    {
      if (Volatile.Read(ref this._disposed))
        return Disposable.Empty;
      lock (this._gate)
      {
        if (this._appendOnlyFile == null)
        {
          this._appendOnlyFile = new SelfLog.AppendOnlyFile(this._path);
          this._refCount = 1;
        }
        else
          ++this._refCount;
      }
      return Disposable.Create(new Action(this.Release));
    }

    private void Release()
    {
      lock (this._gate)
      {
        --this._refCount;
        if (this._refCount > 0)
          return;
        this.DisposeImpl();
      }
    }

    private void DisposeImpl()
    {
      Interlocked.Exchange<SelfLog.AppendOnlyFile>(ref this._appendOnlyFile, (SelfLog.AppendOnlyFile) null)?.Dispose();
    }
  }

  private class AppendOnlyFile : IDisposable
  {
    private const int DefaultBufferSize = 4096 /*0x1000*/;
    private readonly object _syncRoot = new object();
    private readonly string _path;
    private FileStream _stream;
    private int _bufferSize;

    public AppendOnlyFile(string path)
    {
      this._path = path;
      this.CreateFileStream(4096 /*0x1000*/);
    }

    private void CreateFileStream(int bufferSize)
    {
      this._stream = SelfLog.AppendOnlyFile.CreateFileStream(this._path, bufferSize);
      this._bufferSize = bufferSize;
    }

    private static FileStream CreateFileStream(string path, int bufferSize)
    {
      return new FileStream(path, FileMode.Append, FileSystemRights.AppendData, FileShare.ReadWrite, bufferSize, FileOptions.None);
    }

    public static string CheckFileAvailability(string path, bool silent)
    {
      try
      {
        SelfLog.AppendOnlyFile.CreateFileStream(path, 4096 /*0x1000*/).Dispose();
        return path;
      }
      catch (Exception ex) when (silent)
      {
        return (string) null;
      }
    }

    public void Dispose() => this._stream.Dispose();

    public void Write(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      lock (this._syncRoot)
      {
        if (buffer.Length > this._bufferSize)
        {
          FileStream stream = this._stream;
          this.CreateFileStream(buffer.Length);
          stream.Dispose();
        }
        this._stream.Write(buffer, 0, buffer.Length);
        this._stream.Flush();
      }
    }
  }

  private enum Mode
  {
    Off,
    Startup,
    Always,
  }
}
