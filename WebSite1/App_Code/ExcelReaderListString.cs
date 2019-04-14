using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;

public class ExcelReaderListString
{
    public List<List<string>> rowReadAll(string save_address, int sheet_number)//读取excel表格相应工作表的所有数据 仅限 xls文档
    {
        List<List<string>> data = null;
        //如果传入参数合法
        if (!string.IsNullOrEmpty(save_address) && sheet_number > 0)
        {
            //根据文件格式来读取
            //获取后缀
            string extension = Path.GetExtension(save_address);
            if (extension==".csv")
            {
                data = readCsv(save_address);
            }
            else if(extension == ".xls")
            {
                int rowAllCnt = ExcelReaderListString.rowORcolAllCount(save_address, sheet_number, true);
                int colAllCnt = ExcelReaderListString.rowORcolAllCount(save_address, sheet_number, false);
                data = ExcelReaderListString.rowReadSection(save_address, 1, rowAllCnt, 1, colAllCnt, sheet_number);
            }
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
            {// System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, 
             //   System.IO.FileAccess.Read);   无法读取csv 的原因可能是因为编码
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
                            List<string> oneRow = new List<string>();//为相应位置空行创建内存中的空数据行
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

    public static List<List<string>> readCsv(string filePath)//从csv读取数据返回table
    {
        System.Text.Encoding encoding = GetType(filePath); //Encoding.ASCII;//
        DataTable dt = new DataTable();
        System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open,
            System.IO.FileAccess.Read);

        System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding);

        //记录每次读取的一行记录
        string strLine = "";
        //记录每行记录中的各字段内容
        string[] aryLine = null;
        //标示列数
        int columnCount = 0;

        List<DataRow> list = dt.AsEnumerable().ToList();
        List<List<string>> listString = new List<List<string>>();

        //逐行读取CSV中的数据
        while ((strLine = sr.ReadLine()) != null)  //
        {
            aryLine = strLine.Split(',');
            List<string> oneRow = new List<string>();
            columnCount = aryLine.Length;
                for (int j = 0; j < columnCount; j++)
                {
                    oneRow.Add(aryLine[j]);
                }
            listString.Add(oneRow);
        }

        sr.Close();
        fs.Close();
        return listString;
    }
    public static System.Text.Encoding GetType(string FILE_NAME)
{
    System.IO.FileStream fs = new System.IO.FileStream(FILE_NAME, System.IO.FileMode.Open, 
        System.IO.FileAccess.Read);
    System.Text.Encoding r = GetType(fs);
    fs.Close();
    return r;
}
 
/// 通过给定的文件流，判断文件的编码类型
/// <param name="fs">文件流</param>
/// <returns>文件的编码类型</returns>
public static System.Text.Encoding GetType(System.IO.FileStream fs)
{
    byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
    byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
    byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
    System.Text.Encoding reVal = System.Text.Encoding.Default;
 
    System.IO.BinaryReader r = new System.IO.BinaryReader(fs, System.Text.Encoding.Default);
    int i;
    int.TryParse(fs.Length.ToString(), out i);
    byte[] ss = r.ReadBytes(i);
    if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
    {
        reVal = System.Text.Encoding.UTF8;
    }
    else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
    {
        reVal = System.Text.Encoding.BigEndianUnicode;
    }
    else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
    {
        reVal = System.Text.Encoding.Unicode;
    }
    r.Close();
    return reVal;
}

    /// 判断是否是不带 BOM 的 UTF8 格式
    /// <param name="data"></param>
    /// <returns></returns>
    private static bool IsUTF8Bytes(byte[] data)
    {
        int charByteCounter = 1;  //计算当前正分析的字符应还有的字节数
        byte curByte; //当前分析的字节.
        for (int i = 0; i < data.Length; i++)
        {
            curByte = data[i];
            if (charByteCounter == 1)
            {
                if (curByte >= 0x80)
                {
                    //判断当前
                    while (((curByte <<= 1) & 0x80) != 0)
                    {
                        charByteCounter++;
                    }
                    //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　
                    if (charByteCounter == 1 || charByteCounter > 6)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //若是UTF-8 此时第一位必须为1
                if ((curByte & 0xC0) != 0x80)
                {
                    return false;
                }
                charByteCounter--;
            }
        }
        if (charByteCounter > 1)
        {
            throw new Exception("非预期的byte格式");
        }
        return true;
    }
}