using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShuDu.Web.Controllers
{
    public class Solution1Controller : Controller
    {
        // GET: Solution1
        public ActionResult Index(string nd, string y, string m, string d)
        {
            ViewBag.ShuDuHtml = new MvcHtmlString(HttpHelper.GetShuDuHtml(nd, y, m, d));
            return View();
        }

        public ActionResult SolutionOnlyOne(int[] original)
        {
            var dataArray = ConvertToDataArray(original);
            while(true)
            {
                bool isBreak = true;
                for (int i=1;i<=9;i++)
                {
                    SetNotValue(i, dataArray);
                    List<int> indexList = GetOnlyOneIndex(dataArray);
                    foreach(var index in indexList)
                    {
                        dataArray[index].Value = i;
                        isBreak = false;
                    }
                    ClearValue(dataArray);
                }
                if (isBreak)
                {
                    break;
                }
            }
            return Json(dataArray.Select(a => a.Value).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SolutionOnlyOne2(int[] original)
        {
            var dataArray = ConvertToDataArray(original);
            bool isBreak = true;
            while (true)
            {
                for (int i = 1; i <= 9; i++)
                {
                    SetNotValue(i, dataArray);
                    //遍历块
                    for (var j = 0; j < 9; j++)
                    {
                        var temp = dataArray.Where(a => a.Box == j && a.Value == 0);
                        var count = temp.Count();
                        if (count <= 0 || count > 3)
                        {
                            continue;
                        }
                        var temp2 = temp.Select(a => a.Row).Distinct();
                        if (temp2.Count() == 1)
                        {
                            var row = temp2.First();
                            var temp3 = dataArray.Where(a => a.Box != j && a.Row == row && a.Value == 0).Select(a => a).ToArray();
                            if (temp3.Length > 0)
                                SetNotValue(temp3);
                        }
                        temp2 = temp.Select(a => a.Column).Distinct();
                        if (temp2.Count() == 1)
                        {
                            var column = temp2.First();
                            var temp3 = dataArray.Where(a => a.Box != j && a.Column == column && a.Value == 0).Select(a => a).ToArray();
                            if (temp3.Length > 0)
                                SetNotValue(temp3);
                        }
                    }
                    List<int> indexList = GetOnlyOneIndex(dataArray);
                    foreach (var index in indexList)
                    {
                        dataArray[index].Value = i;
                        isBreak = false;
                    }
                    ClearValue(dataArray);
                }
                if(isBreak)
                {
                    break;
                }
            }
            return Json(dataArray.Select(a => a.Value).ToArray(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 唯一值计算综合
        /// </summary>
        public ActionResult SolutionOnlyOne3(int[] original)
        {
            var dataArray = ConvertToDataArray(original);
            OnlyOne(dataArray);
            return Json(dataArray.Select(a => a.Value).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SolutionBacktracking(int[] original)
        {
            var oldData = ConvertToDataArray(original);
            var newData = ConvertToDataArray(original);
            Backtracking(oldData, newData);
            return Json(newData.Select(a => a.Value).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SolutionAuto(int[] original)
        {
            var dataArray = ConvertToDataArray(original);
            OnlyOne(dataArray);
            var oldArray = ConvertToDataArray(dataArray.Select(a => a.Value).ToArray());
            Backtracking(oldArray, dataArray);
            return Json(dataArray.Select(a => a.Value).ToArray(), JsonRequestBehavior.AllowGet);
        }

        private void OnlyOne(Data[] dataArray)
        {
            while (true)
            {
                bool isBreak = true;
                for (int i = 1; i <= 9; i++)
                {
                    SetNotValue(i, dataArray);
                    List<int> indexList = GetOnlyOneIndex(dataArray);
                    foreach (var index in indexList)
                    {
                        dataArray[index].Value = i;
                        isBreak = false;
                    }
                    SetNotValue(i, dataArray);
                    //遍历块
                    for (var j = 0; j < 9; j++)
                    {
                        var temp = dataArray.Where(a => a.Box == j && a.Value == 0);
                        var count = temp.Count();
                        if (count <= 0 || count > 3)
                        {
                            continue;
                        }
                        var temp2 = temp.Select(a => a.Row).Distinct();
                        if (temp2.Count() == 1)
                        {
                            var row = temp2.First();
                            var temp3 = dataArray.Where(a => a.Box != j && a.Row == row && a.Value == 0).Select(a => a).ToArray();
                            if (temp3.Length > 0)
                                SetNotValue(temp3);
                        }
                        temp2 = temp.Select(a => a.Column).Distinct();
                        if (temp2.Count() == 1)
                        {
                            var column = temp2.First();
                            var temp3 = dataArray.Where(a => a.Box != j && a.Column == column && a.Value == 0).Select(a => a).ToArray();
                            if (temp3.Length > 0)
                                SetNotValue(temp3);
                        }
                    }
                    indexList = GetOnlyOneIndex(dataArray);
                    foreach (var index in indexList)
                    {
                        dataArray[index].Value = i;
                        isBreak = false;
                    }
                    ClearValue(dataArray);
                }
                if (isBreak)
                {
                    break;
                }
            }
        }

        private void Backtracking(Data[] oldData,Data[] newData)
        {
            var isBack = false;
            for (var i = 0; i < 81; i++)
            {
                if (oldData[i].Value > 0)
                {
                    if (isBack)
                    {
                        i = i - 2;
                    }
                    continue;
                }
                isBack = false;
                do
                {
                    newData[i].Value = newData[i].Value + 1;
                } while (newData[i].Value <= 9 && !CheckValue(newData[i].Value, newData[i], newData));
                if (newData[i].Value > 9)
                {
                    newData[i].Value = 0;
                    i = i - 2;
                    isBack = true;
                }
            }
        }

        private void ClearValue(Data[] dataArray)
        {
            foreach(var item in dataArray)
            {
                if(item.Value<0)
                {
                    item.Value = 0;
                }
            }
        }

        private List<int> GetOnlyOneIndex(Data[] dataArray)
        {
            List<int> indexList = new List<int>();
            for (int j = 0; j < 9; j++)
            {
                var temp = dataArray.Where(a => a.Box == j && a.Value == 0).Select(a => a);
                if (temp.Count() == 1)
                {
                    indexList.Add(temp.FirstOrDefault().Index);
                }
                temp = dataArray.Where(a => a.Row == j && a.Value == 0).Select(a => a);
                if (temp.Count() == 1)
                {
                    indexList.Add(temp.FirstOrDefault().Index);
                }
                temp = dataArray.Where(a => a.Column == j && a.Value == 0).Select(a => a);
                if (temp.Count() == 1)
                {
                    indexList.Add(temp.FirstOrDefault().Index);
                }
            }
            return indexList;
        }

        private void SetNotValue(int value,Data[] dataArray)
        {
            var temp = dataArray.Where(a => a.Value == value).Select(a => a);
            foreach(var item in temp)
            {
                var temp2 = dataArray.Where(a => a.Box == item.Box && a.Value == 0).Select(a => a).ToArray();
                SetNotValue(temp2);
                temp2 = dataArray.Where(a => a.Row == item.Row && a.Value == 0).Select(a => a).ToArray();
                SetNotValue(temp2);
                temp2 = dataArray.Where(a => a.Column == item.Column && a.Value == 0).Select(a => a).ToArray();
                SetNotValue(temp2);
            }
        }

        private void SetNotValue(Data[] dataArray)
        {
            foreach(var item in dataArray)
            {
                item.Value = -10;
            }
        }

        private bool CheckValue(int value,Data data,Data[] dataArray)
        {
            //同块
            if(dataArray.Where(a=>a.Box==data.Box&&a.Value==value).Count()>1)
            {
                return false;
            }
            //同行
            if (dataArray.Where(a => a.Row == data.Row && a.Value == value).Count() > 1)
            {
                return false;
            }
            //同列
            if (dataArray.Where(a => a.Column == data.Column && a.Value == value).Count() > 1)
            {
                return false;
            }
            return true;
        }

        private Data[] ConvertToDataArray(int[] dataArray)
        {
            Data[] result = new Data[dataArray.Length];
            for(int i=0;i<dataArray.Length;i++)
            {
                var temp = new Data();
                temp.Row = i / 9;
                temp.Column = i % 9;
                temp.Box = (i / 9 / 3) * 3 + i % 9 / 3;
                temp.Value = dataArray[i];
                temp.Index = i;
                result[i] = temp;
            }
            return result;
        }

        private class Data
        {
            public int Index { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }
            public int Box { get; set; }
            public int Value { get; set; }
        }
    }
}