// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXImpersonationScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Win32.SafeHandles;
using System;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;

#nullable disable
namespace PX.Data.Update;

internal class PXImpersonationScope : IDisposable
{
  private const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
  private const int LOGON32_LOGON_INTERACTIVE = 2;
  private const int LOGON32_PROVIDER_DEFAULT = 0;
  private readonly WindowsImpersonationContext PrevIdentity;

  [DllImport("advapi32.dll", SetLastError = true)]
  private static extern bool LogonUser(
    string lpszUserName,
    string lpszDomain,
    string lpszPassword,
    int dwLogonType,
    int dwLogonProvider,
    ref PXImpersonationScope.SafeTokenHandle phToken);

  [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern bool RevertToSelf();

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  private static extern bool CloseHandle(IntPtr handle);

  public PXImpersonationScope(NetworkCredential creds)
    : this(creds.UserName, creds.Domain, creds.Password)
  {
  }

  public PXImpersonationScope(PXCredentials creds)
    : this(creds.UserName, creds.Domain, creds.Password)
  {
  }

  public PXImpersonationScope(string login, string password)
    : this(new PXCredentials(login, password))
  {
  }

  public PXImpersonationScope(string login, string domain, string password)
  {
    PXImpersonationScope.SafeTokenHandle phToken = new PXImpersonationScope.SafeTokenHandle();
    if (!PXImpersonationScope.LogonUser(login, domain, password, 2, 0, ref phToken))
      throw new PXException("The user '{0}' can’t be logged in. The operation failed with the following error code: {1}", new object[2]
      {
        (object) login,
        (object) Marshal.GetLastWin32Error()
      });
    using (phToken)
      this.PrevIdentity = new WindowsIdentity(phToken.DangerousGetHandle()).Impersonate();
  }

  public void Dispose()
  {
    if (this.PrevIdentity == null)
      return;
    this.PrevIdentity.Undo();
  }

  private sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal SafeTokenHandle()
      : base(true)
    {
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseHandle(IntPtr handle);

    protected override bool ReleaseHandle()
    {
      return PXImpersonationScope.SafeTokenHandle.CloseHandle(this.handle);
    }
  }
}
