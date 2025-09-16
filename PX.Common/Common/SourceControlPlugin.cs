// Decompiled with JetBrains decompiler
// Type: PX.Common.SourceControlPlugin
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Xml;

#nullable enable
namespace PX.Common;

public static class SourceControlPlugin
{
  private static ISourceControlPlugin? \u0002;
  public static readonly string configPath = HostingEnvironment.MapPath("~/Customization/SourceControl/config.xml");
  public static readonly string templatePath = HostingEnvironment.MapPath("~/Customization/SourceControl/configTemplate.xml");

  static SourceControlPlugin()
  {
    if (!File.Exists(SourceControlPlugin.configPath))
      return;
    try
    {
      XmlDocument doc = new XmlDocument();
      doc.XmlResolver = (XmlResolver) null;
      doc.Load(SourceControlPlugin.configPath);
      SourceControlPlugin.Init(doc);
    }
    catch
    {
    }
  }

  public static void Init(XmlDocument doc)
  {
    ISourceControlPlugin instance = (ISourceControlPlugin) Activator.CreateInstance(Assembly.LoadFrom(HostingEnvironment.MapPath("~/Customization/SourceControl/" + doc.DocumentElement.GetAttribute("Plugin"))).GetType(doc.DocumentElement.GetAttribute("TypeName"), true));
    instance.Configure(doc.DocumentElement);
    SourceControlPlugin.\u0002 = instance;
  }

  public static void WriteFile(string path, byte[] content)
  {
    if (content == null)
    {
      if (!File.Exists(path))
        return;
      if (SourceControlPlugin.\u0002 != null)
        SourceControlPlugin.\u0002.Checkout(path);
      File.Delete(path);
    }
    else
    {
      if (File.Exists(path))
      {
        byte[] numArray = File.ReadAllBytes(path);
        if (numArray.Length == content.Length)
        {
          bool flag = false;
          for (int index = 0; index < content.Length; ++index)
          {
            if ((int) content[index] != (int) numArray[index])
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            return;
        }
      }
      if (SourceControlPlugin.\u0002 != null)
        SourceControlPlugin.\u0002.Checkout(path);
      File.WriteAllBytes(path, content);
    }
  }

  public static void Checkout(string path)
  {
    if (SourceControlPlugin.\u0002 == null)
      return;
    SourceControlPlugin.\u0002.Checkout(path);
  }

  public static T GetFormat<T>(this IFormatProvider provider)
  {
    return (T) ExceptionExtensions.CheckIfNull<IFormatProvider>(provider, nameof (provider), (string) null).GetFormat(typeof (T));
  }
}
