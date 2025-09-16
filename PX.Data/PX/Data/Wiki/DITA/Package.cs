// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.Package
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Archiver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class Package
{
  private const string DitamapTypesFile = ".ditamap";
  private readonly string[] _dtdfilespath = new string[98]
  {
    "base/dtd/basemap.dtd",
    "base/dtd/basetopic.dtd",
    "base/dtd/commonElements.ent",
    "base/dtd/commonElements.mod",
    "base/dtd/delayResolutionDomain.ent",
    "base/dtd/delayResolutionDomain.mod",
    "base/dtd/hazardstatementDomain.ent",
    "base/dtd/hazardstatementDomain.mod",
    "base/dtd/highlightDomain.ent",
    "base/dtd/highlightDomain.mod",
    "base/dtd/indexingDomain.ent",
    "base/dtd/indexingDomain.mod",
    "base/dtd/map.mod",
    "base/dtd/mapGroup.ent",
    "base/dtd/mapGroup.mod",
    "base/dtd/metaDecl.mod",
    "base/dtd/tblDecl.mod",
    "base/dtd/topic.mod",
    "base/dtd/topicDefn.ent",
    "base/dtd/utilitiesDomain.ent",
    "base/dtd/utilitiesDomain.mod",
    "bookmap/dtd/bookmap.dtd",
    "bookmap/dtd/bookmap.ent",
    "bookmap/dtd/bookmap.mod",
    "ditaval/dtd/ditaval.dtd",
    "learning/dtd/learningAssessment.dtd",
    "learning/dtd/learningAssessment.ent",
    "learning/dtd/learningAssessment.mod",
    "learning/dtd/learningBase.ent",
    "learning/dtd/learningBase.mod",
    "learning/dtd/learningBookmap.dtd",
    "learning/dtd/learningContent.dtd",
    "learning/dtd/learningContent.ent",
    "learning/dtd/learningContent.mod",
    "learning/dtd/learningDomain.ent",
    "learning/dtd/learningDomain.mod",
    "learning/dtd/learningInteractionBaseDomain.ent",
    "learning/dtd/learningInteractionBaseDomain.mod",
    "learning/dtd/learningMap.dtd",
    "learning/dtd/learningMapDomain.ent",
    "learning/dtd/learningMapDomain.mod",
    "learning/dtd/learningMetadataDomain.ent",
    "learning/dtd/learningMetadataDomain.mod",
    "learning/dtd/learningOverview.dtd",
    "learning/dtd/learningOverview.ent",
    "learning/dtd/learningOverview.mod",
    "learning/dtd/learningPlan.dtd",
    "learning/dtd/learningPlan.ent",
    "learning/dtd/learningPlan.mod",
    "learning/dtd/learningSummary.dtd",
    "learning/dtd/learningSummary.ent",
    "learning/dtd/learningSummary.mod",
    "machineryIndustry/dtd/machineryTask.dtd",
    "machineryIndustry/dtd/machineryTaskbodyConstraint.mod",
    "subjectScheme/dtd/classifyDomain.ent",
    "subjectScheme/dtd/classifyDomain.mod",
    "subjectScheme/dtd/classifyMap.dtd",
    "subjectScheme/dtd/subjectScheme.dtd",
    "subjectScheme/dtd/subjectScheme.ent",
    "subjectScheme/dtd/subjectScheme.mod",
    "technicalContent/dtd/abbreviateDomain.ent",
    "technicalContent/dtd/abbreviateDomain.mod",
    "technicalContent/dtd/concept.dtd",
    "technicalContent/dtd/concept.ent",
    "technicalContent/dtd/concept.mod",
    "technicalContent/dtd/ditabase.dtd",
    "technicalContent/dtd/generalTask.dtd",
    "technicalContent/dtd/glossary.dtd",
    "technicalContent/dtd/glossary.ent",
    "technicalContent/dtd/glossary.mod",
    "technicalContent/dtd/glossentry.dtd",
    "technicalContent/dtd/glossentry.ent",
    "technicalContent/dtd/glossentry.mod",
    "technicalContent/dtd/glossgroup.dtd",
    "technicalContent/dtd/glossgroup.ent",
    "technicalContent/dtd/glossgroup.mod",
    "technicalContent/dtd/glossrefDomain.ent",
    "technicalContent/dtd/glossrefDomain.mod",
    "technicalContent/dtd/map.dtd",
    "technicalContent/dtd/programmingDomain.ent",
    "technicalContent/dtd/programmingDomain.mod",
    "technicalContent/dtd/reference.dtd",
    "technicalContent/dtd/reference.ent",
    "technicalContent/dtd/reference.mod",
    "technicalContent/dtd/softwareDomain.ent",
    "technicalContent/dtd/softwareDomain.mod",
    "technicalContent/dtd/strictTaskbodyConstraint.mod",
    "technicalContent/dtd/task.dtd",
    "technicalContent/dtd/task.ent",
    "technicalContent/dtd/task.mod",
    "technicalContent/dtd/taskreqDomain.ent",
    "technicalContent/dtd/taskreqDomain.mod",
    "technicalContent/dtd/topic.dtd",
    "technicalContent/dtd/uiDomain.ent",
    "technicalContent/dtd/uiDomain.mod",
    "xnal/dtd/xnalDomain.ent",
    "xnal/dtd/xnalDomain.mod",
    "catalog-dita.xml"
  };
  private const string _IS_NOT_READABLE = "The stream is not readable.";
  private const string _CANNOT_READ_FILE = "Cannot read the file.";
  private static readonly CultureInfo _READWRITE_CULTURE = CultureInfo.GetCultureInfo("en-us");
  private readonly TopicCollection _topics;
  private DitaMapCollection _maps;
  private readonly FileManager _filemanager;
  private readonly Dictionary<Topic, List<Package.MyFileInfo>> _images;
  private DitaImageReader _imageReader;
  private DitaTopicReader _topicReader;
  private DitaMapReader _mapReader;
  private bool _wasLoad;

  public Package()
  {
    this._topics = new TopicCollection();
    this._filemanager = new FileManager();
    this._maps = new DitaMapCollection(this._topics, this._filemanager);
    this._images = new Dictionary<Topic, List<Package.MyFileInfo>>();
    this._imageReader = new DitaImageReader();
    this._topicReader = new DitaTopicReader();
    this._mapReader = new DitaMapReader();
  }

  public DitaMapCollection Maps
  {
    get => this._maps;
    set => this._maps = value;
  }

  public static byte[] ToBytes(Stream stream)
  {
    long position = stream.Position;
    stream.Position = 0L;
    byte[] buffer = new byte[stream.Length];
    stream.Read(buffer, 0, buffer.Length);
    stream.Position = position;
    return buffer;
  }

  public void Read(Stream os, Guid WikiID)
  {
    ExportContext exportContext = new ExportContext();
    string nativename = "";
    this._wasLoad = !this._wasLoad ? true : throw new InvalidOperationException("It's not allowed to read more than once.");
    if (!os.CanRead)
      throw new ArgumentException("The stream is not readable.");
    try
    {
      using (ZipArchiveWrapper archive = new ZipArchiveWrapper(os))
      {
        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = Package._READWRITE_CULTURE;
        List<ZipArchiveEntry> zipArchiveEntryList = new List<ZipArchiveEntry>();
        foreach (ZipArchiveEntry zipArchiveEntry in archive.GetEntries().ToList<ZipArchiveEntry>())
        {
          if (zipArchiveEntry.Name.IndexOf(".") > -1)
            zipArchiveEntryList.Add(zipArchiveEntry);
        }
        int number = 1;
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchiveEntryList)
        {
          nativename = zipArchiveEntry.Name;
          int num = nativename.LastIndexOf("Shared/");
          if (num > -1)
          {
            nativename = nativename.Remove(0, num + 7);
            if (zipArchiveEntry.Name.IndexOf(".dita") > -1 && zipArchiveEntry.Name.IndexOf(".ditamap") <= -1)
            {
              exportContext.WikiID = WikiID;
              this._topicReader = new DitaTopicReader(exportContext);
              Package.SafelyReadFile(archive, zipArchiveEntry.Name, (Package.ReadFileHandler) (stream => this._topicReader.Read(stream, nativename, number)));
              number++;
              exportContext = this._topicReader._exportContext;
            }
          }
        }
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchiveEntryList)
        {
          ZipArchiveEntry zipfile = zipArchiveEntry;
          nativename = zipfile.Name;
          int num = nativename.LastIndexOf("/");
          if (num > -1)
            nativename = nativename.Remove(0, num + 1);
          if (zipfile.Name.IndexOf(".dita") > -1 && zipfile.Name.IndexOf(".ditamap") <= -1)
          {
            exportContext.WikiID = WikiID;
            this._topicReader = new DitaTopicReader(exportContext);
            Package.SafelyReadFile(archive, zipfile.Name, (Package.ReadFileHandler) (stream => this._topicReader.Read(stream, nativename, number)));
            number++;
          }
          else
          {
            this._imageReader = new DitaImageReader();
            Package.SafelyReadFile(archive, zipfile.Name, (Package.ReadFileHandler) (stream => this._imageReader.Read(stream, zipfile.Name)));
          }
        }
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchiveEntryList)
        {
          nativename = zipArchiveEntry.Name;
          int num = nativename.LastIndexOf("/");
          if (num > -1)
            nativename = nativename.Remove(0, num + 1);
          if (zipArchiveEntry.Name.IndexOf(".ditamap") > -1)
          {
            this._mapReader = new DitaMapReader();
            exportContext.WikiID = WikiID;
            Package.SafelyReadFile(archive, zipArchiveEntry.Name, (Package.ReadFileHandler) (stream => this._mapReader.Read(stream, nativename)));
          }
        }
        Thread.CurrentThread.CurrentCulture = currentCulture;
      }
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      throw new Exception("Cannot read the file. " + nativename, ex);
    }
  }

  public void Read(byte[] buffer, Guid WikiID)
  {
    using (Stream os = (Stream) new MemoryStream(buffer, false))
      this.Read(os, WikiID);
  }

  private static void SafelyReadFile(
    ZipArchiveWrapper archive,
    string fileName,
    Package.ReadFileHandler handler)
  {
    if (string.IsNullOrEmpty(fileName))
      return;
    Stream file = (Stream) null;
    try
    {
      file = archive.GetStream(fileName);
      handler(file);
    }
    catch (TargetInvocationException ex)
    {
    }
    finally
    {
      file?.Dispose();
    }
  }

  public void Write(ZipArchiveWrapper archive)
  {
    foreach (string path in this._dtdfilespath)
    {
      byte[] buffer = this.ReadResource("PX.Data.Wiki.DITA.Schems." + Package.RemovePath(path));
      using (Stream stream = archive.GetStream(path))
        stream.Write(buffer, 0, buffer.Length);
    }
    foreach (KeyValuePair<Topic, List<Package.MyFileInfo>> image in this._images)
    {
      Topic key = image.Key;
      foreach (Package.MyFileInfo file in image.Value)
      {
        string nameByFile = this._filemanager.GetNameByFile(file, key);
        try
        {
          using (Stream stream = archive.GetStream(nameByFile))
            stream.Write(file.Owndata, 0, file.Owndata.Length);
        }
        catch (Exception ex)
        {
        }
      }
    }
    foreach (Topic topic in this._topics)
    {
      string fullName = this._filemanager.GetFullName(topic);
      try
      {
        using (Stream stream = archive.GetStream(fullName))
          topic.Write(stream, this._filemanager);
      }
      catch (Exception ex)
      {
      }
    }
    foreach (DitaMap map in this._maps)
    {
      using (Stream stream = archive.GetStream(map.MapName + ".ditamap"))
        map.Write(stream, this._filemanager);
    }
  }

  public void AddImagesToPackage(Topic topic, IEnumerable<Package.MyFileInfo> images)
  {
    List<Package.MyFileInfo> myFileInfoList = new List<Package.MyFileInfo>();
    if (!this._images.ContainsKey(topic))
      this._images.Add(topic, myFileInfoList);
    this._images[topic].AddRange(images);
    foreach (Package.MyFileInfo image in images)
      this._filemanager.AddFile(image, topic);
  }

  private static Stream GetResource(string path, Assembly targetAssembly)
  {
    if (targetAssembly != (Assembly) null)
    {
      Stream manifestResourceStream = targetAssembly.GetManifestResourceStream(path);
      if (manifestResourceStream != null)
        return manifestResourceStream;
    }
    return (Stream) null;
  }

  private byte[] ReadResource(string path)
  {
    return Package.ReadResource(path, Assembly.GetAssembly(this.GetType()));
  }

  private static byte[] ReadResource(string path, Assembly assembly)
  {
    using (Stream resource = Package.GetResource(path, assembly))
    {
      byte[] buffer = new byte[resource.Length];
      resource.Read(buffer, 0, buffer.Length);
      return buffer;
    }
  }

  private static string RemovePath(string path)
  {
    int num = path.LastIndexOf("/");
    if (num > -1)
      path = path.Remove(0, num + 1);
    return path;
  }

  public class TempCalcLink
  {
    public object tempcalclink;

    public TempCalcLink() => this.tempcalclink = new object();

    public object GetLink() => this.tempcalclink;

    public void SetLink(object bobject) => this.tempcalclink = bobject;
  }

  public class MyFileInfo
  {
    public byte[] Owndata;
    private string Fullname;
    public Guid Guid;
    public bool IsImage;

    public MyFileInfo(byte[] bytes, string fullname, Guid guid)
    {
      this.Owndata = new byte[bytes.Length];
      this.Owndata = bytes;
      this.Fullname = fullname;
      this.Guid = guid;
      this.IsImage = this.AnalyzeType(fullname);
    }

    public MyFileInfo()
    {
      this.Owndata = new byte[0];
      this.Fullname = "";
      this.Guid = Guid.NewGuid();
    }

    public string FullName
    {
      get => this.Fullname;
      set
      {
        this.Fullname = value;
        this.IsImage = this.AnalyzeType(value);
      }
    }

    private bool AnalyzeType(string filename)
    {
      string lower = filename.ToLower();
      string[] strArray = new string[10]
      {
        ".jpg",
        ".jpeg",
        ".jpe",
        ".gif",
        ".png",
        ".bmp",
        ".tif",
        ".tiff",
        ".svg",
        ".ico"
      };
      foreach (string str in strArray)
      {
        if (lower.IndexOf(str) > -1)
          return true;
      }
      return false;
    }
  }

  public delegate void ReadFileHandler(Stream file);
}
