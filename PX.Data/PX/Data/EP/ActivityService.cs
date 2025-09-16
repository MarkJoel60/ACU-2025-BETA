// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.ActivityService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.EP;

/// <exclude />
public static class ActivityService
{
  /// <exclude />
  public interface IActivityType
  {
    #nullable disable
    string Description { get; }

    string Type { get; }

    bool? IsDefault { get; }
  }

  /// <exclude />
  public class Total : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _ScreenID;
    protected int? _Position;
    protected string _Title;
    protected string _ImageKey;
    protected string _ImageSet;
    protected string _Url;
    protected int? _Count;
    protected int? _NewCount;

    [PXDBString(8, InputMask = "CC.CC.CC.CC", IsKey = true)]
    [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string ScreenID
    {
      get => this._ScreenID;
      set => this._ScreenID = value;
    }

    [PXDefault]
    [PXDBInt]
    public virtual int? Position
    {
      get => this._Position;
      set => this._Position = value;
    }

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string Title
    {
      get => this._Title;
      set => this._Title = value;
    }

    [PXDBString(512 /*0x0200*/)]
    [PXUIField]
    public virtual string ImageKey
    {
      get => this._ImageKey;
      set => this._ImageKey = value;
    }

    [PXDBString(512 /*0x0200*/)]
    [PXUIField]
    public virtual string ImageSet
    {
      get => this._ImageSet;
      set => this._ImageSet = value;
    }

    [PXDBString(512 /*0x0200*/)]
    [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string Url
    {
      get => this._Url;
      set => this._Url = value;
    }

    [PXDBInt]
    public virtual int? Count
    {
      get => this._Count;
      set => this._Count = value;
    }

    [PXDBInt]
    public virtual int? NewCount
    {
      get => this._NewCount;
      set => this._NewCount = value;
    }

    /// <exclude />
    public abstract class screenID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ActivityService.Total.screenID>
    {
    }

    /// <exclude />
    public abstract class position : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ActivityService.Total.position>
    {
    }

    /// <exclude />
    public abstract class title : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ActivityService.Total.title>
    {
    }

    /// <exclude />
    public abstract class imageKey : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ActivityService.Total.imageKey>
    {
    }

    /// <exclude />
    public abstract class imageSet : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ActivityService.Total.imageSet>
    {
    }

    /// <exclude />
    public abstract class url : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ActivityService.Total.url>
    {
    }

    /// <exclude />
    public abstract class count : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ActivityService.Total.count>
    {
    }

    /// <exclude />
    public abstract class newCount : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ActivityService.Total.newCount>
    {
    }
  }
}
