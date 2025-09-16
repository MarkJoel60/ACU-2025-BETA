// Decompiled with JetBrains decompiler
// Type: PX.Common.PXContext
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

#nullable disable
namespace PX.Common;

public static class PXContext
{
  public static readonly PXSessionState Session = new PXSessionState();

  internal static async Task AsyncLocal(Func<Task> _param0)
  {
    using (PXContext.AsyncLocal())
      await _param0();
  }

  [PXInternalUseOnly]
  public static IDisposable AsyncLocal() => (IDisposable) SlotStore.AsyncLocal();

  public static ObjectType SetSlot<ObjectType>(ObjectType value)
  {
    TypeKeyedOperationExtensions.Set<ObjectType>(SlotStore.Instance, value);
    return value;
  }

  public static ObjectType SetSlot<ObjectType>(string key, ObjectType value)
  {
    SlotStore.Instance.Set(key, (object) value);
    return value;
  }

  public static void ClearSlot<ObjectType>()
  {
    TypeKeyedOperationExtensions.Remove<ObjectType>(SlotStore.Instance);
  }

  public static void ClearSlot(string key) => SlotStore.Instance.Remove(key);

  public static ObjectType GetSlot<ObjectType>()
  {
    return TypeKeyedOperationExtensions.Get<ObjectType>(SlotStore.Instance);
  }

  public static ObjectType GetSlot<ObjectType>(string key)
  {
    return SlotStore.Instance.Get<ObjectType>(key);
  }

  public static ObjectType EnsureSlot<ObjectType>(ObjectType value) where ObjectType : class
  {
    return PXContext.GetSlot<ObjectType>() ?? PXContext.SetSlot<ObjectType>(value);
  }

  public static ObjectType EnsureSlot<ObjectType>(Func<ObjectType> valueFactory) where ObjectType : class
  {
    return PXContext.GetSlot<ObjectType>() ?? PXContext.SetSlot<ObjectType>(valueFactory());
  }

  public static ObjectType EnsureSlot<ObjectType>(string key, ObjectType value) where ObjectType : class
  {
    return PXContext.GetSlot<ObjectType>(key) ?? PXContext.SetSlot<ObjectType>(key, value);
  }

  public static ObjectType EnsureSlot<ObjectType>(string key, Func<ObjectType> valueFactory) where ObjectType : class
  {
    return PXContext.GetSlot<ObjectType>(key) ?? PXContext.SetSlot<ObjectType>(key, valueFactory());
  }

  [PXInternalUseOnly]
  public static bool IsModernUiScreen() => SlotStore.Instance.IsModernUiScreen();

  internal static bool IsModernUiScreen(this ISlotStore _param0)
  {
    return _param0.Get<bool>("PXScreenOfModerUi");
  }

  public static string GetScreenID() => SlotStore.Instance.GetScreenID();

  internal static string GetScreenID(this ISlotStore _param0) => _param0.Get<string>("PXScreenID");

  internal static void SetScreenID(this ISlotStore _param0, string _param1, bool _param2 = false)
  {
    if (_param1 == "")
      _param1 = (string) null;
    _param0.Set("PXScreenID", (object) _param1);
    _param0.Set("PXScreenOfModerUi", (object) _param2);
  }

  internal static void SetScreenID(HttpContext _param0, string _param1)
  {
    _param0.Slots().SetScreenID(_param1);
  }

  public static void SetScreenID(string screenID) => SlotStore.Instance.SetScreenID(screenID);

  public static Dictionary<string, string> GetDialogAnswers()
  {
    return PXContext.GetSlot<Dictionary<string, string>>("AskDialogFilter.Answer");
  }

  public static void SetDialogAnswers(Dictionary<string, string> dialogAnswers)
  {
    PXContext.SetSlot<Dictionary<string, string>>("AskDialogFilter.Answer", dialogAnswers);
  }

  public static Guid? GetPrimaryRowForSP() => PXContext.GetSlot<Guid?>("SmartPanels.PrimaryRow");

  public static void SetPrimaryRowForSP(Guid? primaryRow)
  {
    PXContext.SetSlot<Guid?>("SmartPanels.PrimaryRow", primaryRow);
  }

  public static (string dialogName, string containerName, string screenId) GetFromDialogSP()
  {
    return PXContext.GetSlot<(string, string, string)>("SmartPanels.FromDialog");
  }

  public static void SetFromDialogSP(
    (string dialogName, string containerName, string screenId) data)
  {
    PXContext.SetSlot<(string, string, string)>("SmartPanels.FromDialog", data);
  }

  public static DateTime SitemapTimeStamp
  {
    get => PXContext.GetSlot<DateTime>(nameof (SitemapTimeStamp));
    set => PXContext.SetSlot<DateTime>(nameof (SitemapTimeStamp), value);
  }

  public static int? GetBranchID() => PXContext.PXIdentity.BranchID;

  public static void SetBranchID(int? branchID) => PXContext.PXIdentity.BranchID = branchID;

  public static DateTime? GetBusinessDate() => PXContext.PXIdentity.BusinessDate;

  public static void SetBusinessDate(DateTime? date) => PXContext.PXIdentity.BusinessDate = date;

  [PXInternalUseOnly]
  public static void SetMobileOfflineMode(PXContext.MobileOfflineMode? offlineModeState)
  {
    PXContext.SetSlot<PXContext.MobileOfflineMode?>("PXMobileOfflineMode", offlineModeState);
  }

  [PXInternalUseOnly]
  public static bool IsInMobileOfflineMode()
  {
    PXContext.MobileOfflineMode? slot = PXContext.GetSlot<PXContext.MobileOfflineMode?>("PXMobileOfflineMode");
    return slot.GetValueOrDefault() == PXContext.MobileOfflineMode.Crawling || slot.GetValueOrDefault() == PXContext.MobileOfflineMode.Executing;
  }

  [PXInternalUseOnly]
  public static bool IsInMobileCrawlingMode()
  {
    return PXContext.GetSlot<PXContext.MobileOfflineMode?>("PXMobileOfflineMode").GetValueOrDefault() == PXContext.MobileOfflineMode.Crawling;
  }

  public static void SetSlot(string key, object value) => SlotStore.Instance.Set(key, value);

  [PXInternalUseOnly]
  public static void ClearAllSlots() => SlotStore.ClearAll();

  public static T SessionTyped<T>() where T : PXSessionState, new() => new T();

  public static PXSessionContext PXIdentity
  {
    get => PXSessionContextFactory.GetSessionContext(SlotStore.Instance);
  }

  internal static void AssertHttpContext()
  {
    if (HttpContext.Current == null)
      throw new InvalidOperationException("No HttpContext available, check for synchronization context loss");
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0002 : IAsyncStateMachine
  {
    public int \u0002;
    public AsyncTaskMethodBuilder \u000E;
    public Func<Task> \u0006;
    private IDisposable \u0008;
    private TaskAwaiter \u0003;

    void IAsyncStateMachine.MoveNext()
    {
      int num = this.\u0002;
      try
      {
        if (num != 0)
          this.\u0008 = PXContext.AsyncLocal();
        try
        {
          TaskAwaiter awaiter;
          if (num != 0)
          {
            awaiter = this.\u0006().GetAwaiter();
            if (!awaiter.IsCompleted)
            {
              this.\u0002 = num = 0;
              this.\u0003 = awaiter;
              this.\u000E.AwaitUnsafeOnCompleted<TaskAwaiter, PXContext.\u0002>(ref awaiter, ref this);
              return;
            }
          }
          else
          {
            awaiter = this.\u0003;
            this.\u0003 = new TaskAwaiter();
            this.\u0002 = num = -1;
          }
          awaiter.GetResult();
        }
        finally
        {
          if (num < 0 && this.\u0008 != null)
            this.\u0008.Dispose();
        }
        this.\u0008 = (IDisposable) null;
      }
      catch (Exception ex)
      {
        this.\u0002 = -2;
        this.\u000E.SetException(ex);
        return;
      }
      this.\u0002 = -2;
      this.\u000E.SetResult();
    }

    [DebuggerHidden]
    void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine _param1)
    {
      this.\u000E.SetStateMachine(_param1);
    }
  }

  public enum MobileOfflineMode
  {
    None,
    Crawling,
    Executing,
  }
}
