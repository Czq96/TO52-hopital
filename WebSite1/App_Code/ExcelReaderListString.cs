using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

public class ExcelReaderListString
{
    public List<List<string>> rowReadAll(string save_address, int sheet_number)//读取excel表格相应工作表的所有数据
    {
        List<List<string>> data = null;
        //如果传入参数合法
        if (!string.IsNullOrEmpty(save_address) && sheet_number > 0)
        {
            int rowAllCnt = ExcelReaderListString.rowORcolAllCount(save_address, sheet_number, true);
            int colAllCnt = ExcelReaderListString.rowORcolAllCount(save_address, sheet_number, false);
            data = ExcelReaderListString.rowReadSection(save_address, 1, rowAllCnt, 1, colAllCnt, sheet_number);
        }
        return data;
    }

    public static int rowORcolAllCount(string save_address, int sheet_number, Boolean readFlag)//读取excel表格相应工作表的所有数据
    {
        int rowORcolCnt = -1;//初始化为-1
        FileStream readfile = null;
        try
        {
            //如果传入参数合法
            if (!string.IsNullOrEmpty(save_address) && sheet_number > 0)
            {
                readfile = new FileStream(save_address, FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                ISheet sheet = hssfworkbook.GetSheetAt(sheet_number - 1);
                if (sheet != null)
                {
                    if (readFlag)//如果需要读取‘有效行数’
                    {
                        rowORcolCnt = sheet.LastRowNum + 1;//有效行数(NPOI读取的有效行数不包括列头，所以需要加1)
                    }
                    else
                    { //如果需要读取‘最大有效列数’
                        for (int rowCnt = sheet.FirstRowNum; rowCnt <= sheet.LastRowNum; rowCnt++)//迭代所有行
                        {
                            IRow row = sheet.GetRow(rowCnt);
                            if (row != null && row.LastCellNum > rowORcolCnt)
                            {
                                rowORcolCnt = row.LastCellNum;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("NpoiOperateExcel.rowOrColumnAllCount方法产生了异常！");
        }
        finally
        {
            if (readfile != null) { readfile.Close(); }
        }
        return rowORcolCnt;
    }



    public static List<List<string>> rowReadSection(string save_address, int start_row, int stop_row,

          int sart_column, int stop_column, int sheet_number)//读取excel表格相应工作表的部分数据
    {

        List<List<string>> data = null;//初始化为空

        FileStream readfile = null;
        try
        {
            //如果传入参数合法

            if (!string.IsNullOrEmpty(save_address) && start_row > 0 && stop_row > 0 && sart_column > 0 && stop_column > 0 && sheet_number > 0)

            {
                readfile = new FileStream(save_address, FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                ISheet sheet = hssfworkbook.GetSheetAt(sheet_number - 1);
                if (sheet != null)
                {
                    for (int rowIndex = start_row - 1; rowIndex < stop_row; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (row != null)
                        {
                            List<string> oneRow = new List<string>();
                            for (int columnIndex = sart_column - 1; columnIndex < stop_column; columnIndex++)
                            {
                                ICell cell = row.GetCell(columnIndex);
                                if (cell != null)
                                {
                                    oneRow.Add(cell.ToString());
                                }
                                else
                                {
                                    oneRow.Add("");//填充空的数据
                                }
                            }
                            if (data == null)
                            {
                                data = new List<List<string>>();//初始化
                            }
                            data.Add(oneRow);
                        }
                        else
                        {
                            List<string> oneRow = new List<string>();//软件为相应位置空行创建内存中的空数据行
                            for (int columnIndex = sart_column - 1; columnIndex < stop_column; columnIndex++)
                            {
                                oneRow.Add("");//填充空的数据
                            }
                            if (data == null)
                            {
                                data = new List<List<string>>();//初始化
                            }
                            data.Add(oneRow);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("NpoiOperateExcel.rowReadSection方法产生了异常！" + e.Message);
        }
        finally
        {
            if (readfile != null) { readfile.Close(); }
        }
        return data;
    }

    //static void Main()
    //{
    //    List<List<string>> data = rowReadAll("soins.xls", 1);

    //    for (int i = 0; i < data.Count; i++)
    //    {
    //        for (int j = 0; j < data[i].Count; j++)
    //        {
    //            Console.Write(data[i][j]);

    //        }

    //        if (data[i][1] == "" && data[i][0] != "")
    //        { Console.WriteLine("semaine found"); }
    //        Console.WriteLine();
    //    }
    //}

}