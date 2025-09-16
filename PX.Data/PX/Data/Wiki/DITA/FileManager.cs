// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.FileManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace PX.Data.Wiki.DITA;

internal class FileManager : IFileManager
{
  private readonly HybridDictionary _listUp;
  private readonly HybridDictionary _listDown;
  private readonly HybridDictionary _filelist;

  public FileManager()
  {
    this._listUp = new HybridDictionary();
    this._listDown = new HybridDictionary();
    this._filelist = new HybridDictionary();
  }

  internal void AddTopic(Topic child, Topic parent)
  {
    if (!(parent != null & child != null))
      return;
    child.FileName = child.FileName.Replace(" ", "_");
    parent.FileName = parent.FileName.Replace(" ", "_");
    child.FileName = child.FileName.Replace(":", "_");
    parent.FileName = parent.FileName.Replace(":", "_");
    child.FileName = child.FileName.Replace(",", "_");
    parent.FileName = parent.FileName.Replace(",", "_");
    child.FileName = child.FileName.Replace("&#8221", "_");
    parent.FileName = parent.FileName.Replace("&#8221", "_");
    List<Topic> collection = new List<Topic>() { child };
    if (!this._listUp.Contains((object) parent))
      this._listUp.Add((object) parent, (object) collection);
    else
      ((List<Topic>) this._listUp[(object) parent]).AddRange((IEnumerable<Topic>) collection);
    foreach (object key in collection)
      this._listDown.Add(key, (object) parent);
  }

  internal void AddTopic(Topic child)
  {
    if (child == null)
      return;
    child.FileName = child.FileName.Replace(" ", "_");
    child.FileName = child.FileName.Replace(":", "_");
    child.FileName = child.FileName.Replace(",", "_");
    child.FileName = child.FileName.Replace("&#8221", "_");
    if (this._listDown.Contains((object) child))
      return;
    this._listDown.Add((object) child, (object) null);
  }

  internal void AddFile(Package.MyFileInfo file, Topic parent)
  {
    if (parent == null)
      return;
    file.FullName = file.FullName.Replace(" ", "_");
    parent.FileName = parent.FileName.Replace(" ", "_");
    file.FullName = file.FullName.Replace(":", "_");
    parent.FileName = parent.FileName.Replace(":", "_");
    file.FullName = file.FullName.Replace("&#8221", "_");
    parent.FileName = parent.FileName.Replace("&#8221", "_");
    if (this._filelist.Contains((object) file))
      return;
    this._filelist.Add((object) file, (object) parent);
  }

  internal IEnumerable<Topic> GetChilds(Topic parent)
  {
    try
    {
      return (IEnumerable<Topic>) this._listUp[(object) parent];
    }
    catch
    {
      return (IEnumerable<Topic>) null;
    }
  }

  internal string GetFullName(Topic child)
  {
    string str = $"{child.FileName}/{child.FileName}";
    Topic topic;
    for (; (topic = (Topic) this._listDown[(object) child]) != null; child = topic)
      str = $"{topic.FileName}/{str}";
    return str + ".dita";
  }

  internal string GetFullPath(Topic child)
  {
    string fullPath = "";
    Topic topic;
    for (; (topic = (Topic) this._listDown[(object) child]) != null; child = topic)
      fullPath = $"{topic.FileName}/{fullPath}";
    return fullPath;
  }

  internal string GetNameByFile(Package.MyFileInfo file, Topic currentTopic)
  {
    string nameByFile = "../";
    Topic topic;
    for (Topic key = currentTopic; (topic = (Topic) this._listDown[(object) key]) != null; key = topic)
      nameByFile = "../" + nameByFile;
    if (file != null)
    {
      if (file.IsImage)
        nameByFile = $"{nameByFile}{this.GetFullPath((Topic) this._filelist[(object) file])}{currentTopic.FileName}/Images/{file.FullName}";
      else
        nameByFile = $"{nameByFile}{this.GetFullPath((Topic) this._filelist[(object) file])}{currentTopic.FileName}/Files/{file.FullName}";
    }
    return nameByFile;
  }

  internal string GetNameByTopic(Topic file, Topic currentTopic)
  {
    string nameByTopic = "../";
    Topic topic;
    for (Topic key = currentTopic; (topic = (Topic) this._listDown[(object) key]) != null; key = topic)
      nameByTopic = "../" + nameByTopic;
    if (file != null)
      nameByTopic += this.GetFullName(file);
    return nameByTopic;
  }

  public string GetLink(Package.TempCalcLink file, Topic currentTopic, string anchor)
  {
    string link = (string) null;
    if (file != null)
    {
      if (file.tempcalclink is Topic)
      {
        link = this.GetNameByTopic((Topic) file.tempcalclink, currentTopic);
        if (anchor != "")
        {
          string filename = ((Topic) file.tempcalclink).TopicId.ToString();
          link = $"{$"{link}#topic{FileManager.RemoveSpace(filename)}"}/{anchor}";
        }
      }
      if (file.tempcalclink is Package.MyFileInfo)
      {
        link = this.GetNameByFile((Package.MyFileInfo) file.tempcalclink, currentTopic);
        if (anchor != "")
          link = $"{link}/{anchor}";
      }
    }
    else if (anchor != "")
    {
      string filename = currentTopic.TopicId.ToString();
      link = $"{$"{this.GetNameByTopic(currentTopic, currentTopic)}#topic{FileManager.RemoveSpace(filename)}"}/{anchor}";
    }
    return link;
  }

  internal string GetDtd(Topic currentTopic, string path)
  {
    return this.GetNameByFile((Package.MyFileInfo) null, currentTopic) + path;
  }

  internal static string RemoveSpace(string filename)
  {
    filename = filename.Replace("-", "");
    return filename;
  }

  internal Topic GetParent(Topic topic) => (Topic) this._listDown[(object) topic];
}
