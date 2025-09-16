// Decompiled with JetBrains decompiler
// Type: PX.Data.GoogleMapRedirector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Data;

public class GoogleMapRedirector : MapRedirector
{
  private const string url = "http://maps.google.com/maps?hl={0}";

  public GoogleMapRedirector()
    : base("Google")
  {
  }

  public override void ShowAddress(
    string country,
    string state,
    string city,
    string postalCode,
    string addressLine1,
    string addressLine2,
    string addressLine3)
  {
    this.DoRedirect(country, state, city, postalCode, addressLine1, addressLine2, addressLine3);
  }

  private void DoRedirect(params string[] args)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("http://maps.google.com/maps?hl={0}", (object) Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
    stringBuilder.Append("&q=");
    int length = stringBuilder.Length;
    for (int index = 0; index < args.Length; ++index)
    {
      if (!string.IsNullOrEmpty(args[index]))
      {
        if (stringBuilder.Length > length)
          stringBuilder.Append(",");
        stringBuilder.Append(HttpUtility.UrlPathEncode(args[index]));
      }
    }
    string url = stringBuilder.ToString();
    stringBuilder.Insert(0, PXRedirectHelper.GetRedirectPrefix(PXBaseRedirectException.WindowMode.New, true));
    string message = stringBuilder.ToString();
    throw new PXMapRedirectException(url, PXBaseRedirectException.WindowMode.New, true, message);
  }
}
