// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.Utils.ReportCycleDetector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Reports.Utils;

internal class ReportCycleDetector
{
  private Dictionary<int, List<int>> _graph = new Dictionary<int, List<int>>();
  private Dictionary<int, int> _discoveryTime = new Dictionary<int, int>();
  private Dictionary<int, int> _lowLink = new Dictionary<int, int>();
  private Stack<int> _stack = new Stack<int>();
  private HashSet<int> _inStack = new HashSet<int>();
  private List<List<int>> _cycles = new List<List<int>>();
  private int time;

  public void AddNode(int nodeId, IEnumerable<int> references)
  {
    if (!this._graph.ContainsKey(nodeId))
      this._graph[nodeId] = new List<int>();
    this._graph[nodeId].AddRange(references);
  }

  public IEnumerable<IEnumerable<int>> FindCycles()
  {
    foreach (int key in this._graph.Keys)
    {
      if (!this._discoveryTime.ContainsKey(key))
        this.TarjanDFS(key);
    }
    return (IEnumerable<IEnumerable<int>>) this._cycles;
  }

  private void TarjanDFS(int node)
  {
    this._discoveryTime[node] = this.time;
    this._lowLink[node] = this.time;
    ++this.time;
    this._stack.Push(node);
    this._inStack.Add(node);
    if (this._graph.ContainsKey(node))
    {
      foreach (int num in this._graph[node])
      {
        if (!this._discoveryTime.ContainsKey(num))
        {
          this.TarjanDFS(num);
          this._lowLink[node] = System.Math.Min(this._lowLink[node], this._lowLink[num]);
        }
        else if (this._inStack.Contains(num))
          this._lowLink[node] = System.Math.Min(this._lowLink[node], this._discoveryTime[num]);
      }
    }
    if (this._lowLink[node] != this._discoveryTime[node])
      return;
    List<int> intList = new List<int>();
    int num1;
    do
    {
      num1 = this._stack.Pop();
      this._inStack.Remove(num1);
      intList.Add(num1);
    }
    while (num1 != node);
    if (intList.Count <= 1 && !this._graph[node].Contains(node))
      return;
    this._cycles.Add(intList);
  }
}
