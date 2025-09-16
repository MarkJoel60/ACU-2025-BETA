// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.RoleAccessExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.EP;
using PX.SM;

#nullable disable
namespace PX.Objects.EP;

public class RoleAccessExt : PXGraphExtension<RoleAccess>
{
  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (Users.username))]
  [PXUIField(DisplayName = "Username")]
  [PXParent(typeof (Select<Users, Where<Users.username, Equal<Current<UsersInRoles.username>>>>))]
  [PXSelector(typeof (Search2<Users.username, LeftJoin<EPLoginType, On<EPLoginType.loginTypeID, Equal<Users.loginTypeID>>, LeftJoin<EPLoginTypeAllowsRole, On<EPLoginTypeAllowsRole.loginTypeID, Equal<EPLoginType.loginTypeID>>>>, Where2<Where2<Where<Users.isHidden, Equal<False>>, And<Where2<Where<Users.source, Equal<PXUsersSourceListAttribute.application>, Or<Users.overrideADRoles, Equal<True>>>, And<Where<Current<Roles.guest>, Equal<True>, Or<Users.guest, NotEqual<True>>>>>>>, And<Where<EPLoginTypeAllowsRole.rolename, Equal<Current<UsersInRoles.rolename>>, Or<Users.loginTypeID, IsNull>>>>>), DescriptionField = typeof (Users.comment), DirtyRead = true)]
  protected virtual void UsersInRoles_Username_CacheAttached(PXCache sender)
  {
  }
}
