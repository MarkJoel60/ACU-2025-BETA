// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Inquiry.CRSMEmail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.TM;

#nullable disable
namespace PX.Objects.CR.Inquiry;

[PXCacheName("Email Activity")]
[PXProjection(typeof (Select<PX.Objects.CR.CRSMEmail, Where<PX.Objects.CR.CRSMEmail.classID, Equal<CRActivityClass.email>, And<Where<CRActivity.createdByID, Equal<CurrentValue<AccessInfo.userID>>, Or<PX.Objects.CR.CRSMEmail.ownerID, Equal<CurrentValue<AccessInfo.contactID>>, Or<PX.Objects.CR.CRSMEmail.ownerID, IsSubordinateOfContact<CurrentValue<AccessInfo.contactID>>, Or<PX.Objects.CR.CRSMEmail.workgroupID, IsWorkgroupOfContact<CurrentValue<AccessInfo.contactID>>>>>>>>>))]
public class CRSMEmail : PX.Objects.CR.CRSMEmail
{
}
