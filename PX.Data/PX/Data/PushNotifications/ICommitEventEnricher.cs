// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.ICommitEventEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.PushNotifications;

/// <summary>Allows you to add additional information to push notifications.</summary>
/// <example><para>The following code shows a sample implementation of the ICommitEventEnricher interface, which adds the business date and the name of the user to the AdditionalInfo element of notifications in JSON format.</para>
/// <code title="Example" lang="CS">
/// using PX.Data;
/// using PX.Data.PushNotifications;
/// 
/// public class CommitEventEnricher : ICommitEventEnricher
/// {
///     public void Enrich(IQueueEvent commitEvent)
///     {
///         var businessDate = PXContext.PXIdentity?.BusinessDate;
///         var userName = PXContext.PXIdentity?.IdentityName;
///         commitEvent.AdditionalInfo.Add(nameof(businessDate), businessDate);
///         commitEvent.AdditionalInfo.Add(nameof(userName), userName);
///     }
/// }</code>
/// <code title="Example2" description="The application includes the additional information in the AdditionalInfo element of the notifications in JSON format, as shown in the following notification example." groupname="Example" lang="Json">
/// {
///     ...
///     "TimeStamp":636295833829493672,
///     "AdditionalInfo":
///     {
///         "businessDate":"2017-05-05T15:16:23.1",
///         "userName":"admin"
///     }
/// }</code>
/// </example>
public interface ICommitEventEnricher
{
  /// <summary>You use this method to add properties that you want to be returned in push notifications.</summary>
  /// <remarks>The <tt>Enrich()</tt> method is called in the <tt>PX.Data.PXTransactionScope.Dispose()</tt> method.
  /// Therefore, the Enrich() method must not return data that is not accessible in this scope.</remarks>
  /// <example>
  /// <code title="Example" lang="CS">
  /// //The following code shows a sample implementation of the ICommitEventEnricher interface,
  /// //which adds the business date and the name of the user
  /// //to the AdditionalInfo element of notifications in JSON format.
  /// using PX.Data;
  /// using PX.Data.PushNotifications;
  /// public class CommitEventEnricher : ICommitEventEnricher
  /// {
  ///      public void Enrich(IQueueEvent commitEvent)
  ///      {
  ///          var businessDate = PXContext.PXIdentity?.BusinessDate;
  ///          var userName = PXContext.PXIdentity?.IdentityName;
  ///          commitEvent.AdditionalInfo.Add(nameof(businessDate), businessDate);
  ///          commitEvent.AdditionalInfo.Add(nameof(userName), userName);
  ///      }
  /// }</code>
  /// </example>
  void Enrich(IQueueEvent commitEvent);
}
