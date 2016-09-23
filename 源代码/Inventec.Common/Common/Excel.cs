using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Inventec.Common
{
    /// <summary>
    /// EXCEL操作類
    /// </summary>
    public class Excel
    {
        public Excel() { }

        /// <summary>
        /// 導入EXCEL2010版本數據
        /// </summary>
        /// <param name="FilePath">Excel文件路徑</param>
        /// <param name="FileName">Excel文件名稱</param>
        /// <param name="FileName">標題所在行號</param>
        /// <returns>讀取到Excel數據的DataTable</returns>
        /// <Author>Sherry</Author>
        /// <CreateDate>2013/01/24</CreateDate>
        /// <RevisionHistory>
        /// <ModifyBy></ModifyBy>
        /// <ModifyDate></ModifyDate>
        /// <ModifyReason></ModifyReason>
        /// </RevisionHistory>
        /// <LastModifyDate></LastModifyDate>
        public static DataTable ImportExcelXData(string FilePath, int TitleRowIndex)
        {
            DataTable dtExcel = new DataTable();
            try
            {
                //判斷路徑是否以斜杠結尾
                //if (FilePath.Substring(FilePath.Length - 1) != "\\" && FilePath.Substring(FilePath.Length - 1) != "/")
                //    FilePath = FilePath + "\\";
                //讀取Excel工作薄
                XSSFWorkbook workbook = new XSSFWorkbook(File.Open(FilePath, FileMode.Open));
                //讀取Excel工作區
                XSSFSheet sheet = (XSSFSheet)workbook.GetSheetAt(0);
                //獲取最大行數
                int rowcount = sheet.LastRowNum + 1;
                //獲取標題行最大列數
                int colcount = sheet.GetRow(TitleRowIndex - 1).PhysicalNumberOfCells;
                //在DataTable中建立列
                for (int i = 0; i < colcount; i++)
                    dtExcel.Columns.Add(i.ToString());
                //將Excel數據加入到DataTable中
                for (int x = TitleRowIndex; x < rowcount; x++)
                {
                    DataRow row = dtExcel.NewRow();
                    for (int y = 0; y < colcount; y++)
                    {
                        try
                        {
                            //if (sheet.GetRow(x).GetCell(y).CellType == CellType.NUMERIC)
                            //row[y] = sheet.GetRow(x).GetCell(y).NumericCellValue;
                            //else
                            row[y] = sheet.GetRow(x).GetCell(y).ToString();
                        }
                        catch { row[y] = ""; }
                    }

                    dtExcel.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtExcel;
        }

        /// <summary>
        /// 導入EXCEL2003-2007版本數據
        /// </summary>
        /// <param name="FilePath">Excel文件路徑</param>
        /// <param name="FileName">Excel文件名稱</param>
        /// <param name="FileName">標題所在行號</param>
        /// <returns>讀取到Excel數據的DataTable</returns>
        /// <Author>Sherry</Author>
        /// <CreateDate>2013/01/24</CreateDate>
        /// <RevisionHistory>
        /// <ModifyBy></ModifyBy>
        /// <ModifyDate></ModifyDate>
        /// <ModifyReason></ModifyReason>
        /// </RevisionHistory>
        /// <LastModifyDate></LastModifyDate>
        public static DataTable ImportExcelData(string FilePath, int TitleRowIndex)
        {
            DataTable dtExcel = new DataTable();
            try
            {
                //判斷路徑是否以斜杠結尾
                //if (FilePath.Substring(FilePath.Length - 1) != "\\" && FilePath.Substring(FilePath.Length - 1) != "/")
                //    FilePath = FilePath + "\\";
                //讀取Excel工作薄

                HSSFWorkbook workbook = new HSSFWorkbook(File.Open(FilePath, FileMode.Open));
                //讀取Excel工作區
                HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(0);

                //獲取最大行數
                int rowcount = sheet.LastRowNum + 1;
                //獲取標題行最大列數
                int colcount = sheet.GetRow(TitleRowIndex - 1).PhysicalNumberOfCells;

                //在DataTable中建立列
                for (int i = 0; i < colcount; i++)
                    dtExcel.Columns.Add(i.ToString());
                //將Excel數據加入到DataTable中
                for (int x = TitleRowIndex; x < rowcount; x++)
                {
                    if (sheet.GetRow(x) != null)
                    {
                        DataRow row = dtExcel.NewRow();
                        for (int y = 0; y < colcount; y++)
                        {
                            try
                            {

                                //if (sheet.GetRow(x).GetCell(y).CellType == CellType.NUMERIC)
                                //row[y] = sheet.GetRow(x).GetCell(y).NumericCellValue;
                                //else
                                if (sheet.GetRow(x).GetCell(y) != null)
                                {
                                    row[y] = sheet.GetRow(x).GetCell(y).ToString();
                                }

                            }
                            catch { row[y] = ""; }
                        }

                        dtExcel.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("The supplied data appears to be in the Office 2007+ XML") >= 0)
                {
                    dtExcel = ImportExcelXData(FilePath, TitleRowIndex);
                }
                else
                {
                    throw ex;
                }
            }
            return dtExcel;
        }

        /// <summary>
        /// 導出Excel
        /// </summary>
        /// <param name="dtExcel">需要導出的數據源</param>
        /// <param name="TitleRowIndex">標題行的位置</param>
        /// <returns>Excel內存流</returns>
        /// <Author>Sherry</Author>
        /// <CreateDate>2013/03/12</CreateDate>
        /// <RevisionHistory>
        /// <ModifyBy></ModifyBy>
        /// <ModifyDate></ModifyDate>
        /// <ModifyReason></ModifyReason>
        /// </RevisionHistory>
        /// <LastModifyDate></LastModifyDate>
        public static bool ExportExcel(DataTable dtExcel, int TitleRowIndex, string FileName)
        {
            //創建內存流
            FileStream ms = new FileStream(FileName, FileMode.Create);
            try
            {
                //創建Excel工作薄
                XSSFWorkbook workbook = new XSSFWorkbook();
                //創建Excel工作區
                ISheet sheet = workbook.CreateSheet();
                //sheet.CreateFreezePane(1, 1);// 冻结第一行和第一列
                //創建標題行
                IRow headerRow = sheet.CreateRow(TitleRowIndex - 1);
                //將DataTable中的標題寫入到標題行中
                foreach (DataColumn column in dtExcel.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    headerRow.Height = 25 * 20;
                }
                int RowIndex = TitleRowIndex;
                //從標題行后寫入數據到Excel
                foreach (DataRow row in dtExcel.Rows)
                {
                    IRow dataRow = sheet.CreateRow(RowIndex);
                    //寫入數據到Excel單元格
                    foreach (DataColumn column in dtExcel.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    dataRow.Height = 25 * 20;
                    RowIndex++;
                }

                //列宽自适应，只对英文和数字有效
                for (int i = 0; i <= dtExcel.Rows.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                //获取当前列的宽度，然后对比本列的长度，取最大值
                for (int columnNum = 0; columnNum <= dtExcel.Columns.Count; columnNum++)
                {
                    int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                    {
                        IRow currentRow;
                        //当前行未被使用过
                        if (sheet.GetRow(rowNum) == null)
                        {
                            currentRow = sheet.CreateRow(rowNum);
                        }
                        else
                        {
                            currentRow = sheet.GetRow(rowNum);
                        }

                        if (currentRow.GetCell(columnNum) != null)
                        {
                            ICell currentCell = currentRow.GetCell(columnNum);
                            int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                            if (columnWidth < length)
                            {
                                columnWidth = length;
                            }
                        }
                    }
                    if (columnWidth > 220)
                        columnWidth = 220;
                    sheet.SetColumnWidth(columnNum, columnWidth * 256);
                }
                workbook.Write(ms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// 導出Excel
        /// </summary>
        /// <param name="dtExcel">需要導出的數據源</param>
        /// <param name="TitleRowIndex">標題行的位置</param>
        /// <returns>Excel內存流</returns>
        /// <Author>Sherry</Author>
        /// <CreateDate>2013/03/12</CreateDate>
        /// <RevisionHistory>
        /// <ModifyBy></ModifyBy>
        /// <ModifyDate></ModifyDate>
        /// <ModifyReason></ModifyReason>
        /// </RevisionHistory>
        /// <LastModifyDate></LastModifyDate>
        public static bool ExportAllExcel(DataTable dtExcel, int TitleRowIndex, string FileName)
        {
            //創建內存流
            FileStream ms = new FileStream(FileName, FileMode.Create);
            try
            {
                //創建Excel工作薄
                XSSFWorkbook workbook = new XSSFWorkbook();
                //創建Excel工作區
                ISheet sheet = workbook.CreateSheet();
                //sheet.CreateFreezePane(1, 1);// 冻结第一行和第一列
                //創建標題行
                IRow headerRow = sheet.CreateRow(TitleRowIndex - 1);
                //將DataTable中的標題寫入到標題行中
                foreach (DataColumn column in dtExcel.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    headerRow.Height = 25 * 20;
                }
                int RowIndex = TitleRowIndex;
                //從標題行后寫入數據到Excel
                foreach (DataRow row in dtExcel.Rows)
                {
                    IRow dataRow = sheet.CreateRow(RowIndex);
                    //寫入數據到Excel單元格
                    foreach (DataColumn column in dtExcel.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    dataRow.Height = 25 * 20;
                    RowIndex++;
                }

                //列宽自适应，只对英文和数字有效
                /*for (int i = 0; i <= dtExcel.Rows.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }*/
                //获取当前列的宽度，然后对比本列的长度，取最大值
                for (int columnNum = 0; columnNum <= dtExcel.Columns.Count; columnNum++)
                {
                    int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                    {
                        IRow currentRow;
                        //当前行未被使用过
                        if (sheet.GetRow(rowNum) == null)
                        {
                            currentRow = sheet.CreateRow(rowNum);
                        }
                        else
                        {
                            currentRow = sheet.GetRow(rowNum);
                        }

                        if (currentRow.GetCell(columnNum) != null)
                        {
                            ICell currentCell = currentRow.GetCell(columnNum);
                            int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                            if (columnWidth < length)
                            {
                                columnWidth = length;
                            }
                        }
                    }
                    sheet.SetColumnWidth(columnNum, columnWidth * 300);
                }
                workbook.Write(ms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
