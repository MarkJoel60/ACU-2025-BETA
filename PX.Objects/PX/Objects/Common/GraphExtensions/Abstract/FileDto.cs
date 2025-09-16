// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.FileDto
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.IO;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

internal sealed class FileDto
{
  public const int FileNameMaxLength = 208 /*0xD0*/;

  public FileDto(Guid entityId, string name, byte[] content, Guid? fileId = null, string contentId = null)
  {
    this.EntityNoteId = entityId;
    this.Content = content ?? throw new ArgumentNullException(nameof (content));
    this.Name = name ?? throw new ArgumentNullException(nameof (name));
    try
    {
      string extension = Path.GetExtension(this.Name);
      if (this.Name.Length > 208 /*0xD0*/)
      {
        if (Str.IsNullOrEmpty(extension))
        {
          string str = this.Name.Substring(0, 208 /*0xD0*/);
          PXTrace.WriteWarning("Trying to save file with too long name. The name was cut off. Original value: {0}, new value: {1}", new object[2]
          {
            (object) name,
            (object) str
          });
          this.Name = str;
        }
        else
        {
          string str = this.Name.Substring(0, 208 /*0xD0*/ + extension.Length);
          PXTrace.WriteWarning("Trying to save file with too long name. The name was cut off. Original value: {0}, new value: {1}", new object[2]
          {
            (object) name,
            (object) str
          });
          this.Name = str + extension;
        }
      }
    }
    catch
    {
      PXTrace.WriteWarning("Trying to save file with invalid file name. The name will be replaced with default: 'file'.");
      this.Name = "file";
    }
    this.FileId = fileId ?? Guid.NewGuid();
    this.ContentId = contentId;
  }

  public Guid EntityNoteId { get; }

  public Guid FileId { get; }

  public string Name { get; }

  public string FullName => $"{this.FileId.ToString()}\\{this.Name}";

  public byte[] Content { get; }

  public string ContentId { get; }
}
