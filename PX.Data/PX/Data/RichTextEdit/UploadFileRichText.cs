// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.UploadFileRichText
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;

#nullable enable
namespace PX.Data.RichTextEdit;

public class UploadFileRichText : PXGraph<
#nullable disable
UploadFileRichText>
{
  public PXSelect<UploadFile> Files1;
  public PXSelect<UploadFile, Where<UploadFile.shortName, Like<UploadFileRichText.gif>, Or<UploadFile.shortName, Like<UploadFileRichText.jpg>, Or<UploadFile.shortName, Like<UploadFileRichText.png>, Or<UploadFile.shortName, Like<UploadFileRichText.bmp>>>>>, OrderBy<NoSort>> Images;
  public PXSelect<UploadFile> Files;
  public PXSelectJoin<UploadFile, InnerJoin<UploadFileRevisionNoData, On<UploadFile.lastRevisionID, Equal<UploadFileRevisionNoData.fileRevisionID>, And<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>>>>> FilesWithSize;

  public class png : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.png>
  {
    public png()
      : base("%png")
    {
    }
  }

  public class jpg : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.jpg>
  {
    public jpg()
      : base("%jpg")
    {
    }
  }

  public class gif : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.gif>
  {
    public gif()
      : base("%gif")
    {
    }
  }

  public class bmp : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.bmp>
  {
    public bmp()
      : base("%bmp")
    {
    }
  }

  public class PNG : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.PNG>
  {
    public PNG()
      : base("%PNG")
    {
    }
  }

  public class JPG : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.JPG>
  {
    public JPG()
      : base("%JPG")
    {
    }
  }

  public class GIF : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.GIF>
  {
    public GIF()
      : base("%GIF")
    {
    }
  }

  public class BMP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UploadFileRichText.BMP>
  {
    public BMP()
      : base("%BMP")
    {
    }
  }
}
