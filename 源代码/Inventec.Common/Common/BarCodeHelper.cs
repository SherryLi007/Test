using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BarcodeLib;


namespace Inventec.Common
{
    /// <summary>
    /// 條形碼生成類
    /// </summary>
    public class BarCodeHelper
    {
        private static BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();

        public BarCodeHelper() { }

        /// <summary>
        /// 條形碼對齊方式
        /// </summary>
        public enum BarcodeAlignment
        {
            Left,
            Center,
            Right
        }

        /// <summary>
        /// 條形碼編碼類型 枚舉類型
        /// </summary>
        //public enum BarcodeType
        //{
        //    UPCA,
        //    UPCE,
        //    UPC_SUPPLEMENTAL_2DIGIT,
        //    UPC_SUPPLEMENTAL_5DIGIT,
        //    EAN13,
        //    JAN13,
        //    EAN8,
        //    ITF14,
        //    Codabar,
        //    PostNet,
        //    BOOKLAND,
        //    CODE11,
        //    CODE39,
        //    CODE39Extended,
        //    CODE93,
        //    LOGMARS,
        //    MSI_Mod10,
        //    Interleaved2of5,
        //    Standard2of5,
        //    CODE128,
        //    CODE128A,
        //    CODE128B,
        //    CODE128C,
        //    TELEPEN
        //}

        /// <summary>
        /// 條形碼標籤位置
        /// </summary>
        public enum BarcodeLabelLocation
        {
            BottomLeft,
            BottomCenter,
            BottomRight,
            TopLeft,
            TopCenter,
            TopRight
        }


        /// <summary>
        /// 條形碼生成
        /// </summary>
        /// <param name="Code">條形碼標籤</param>
        /// <param name="Width">條形碼寬度</param>
        /// <param name="Height">條形碼高度</param>
        /// <param name="Alignment">條形碼對齊方式</param>
        /// <param name="LabelLocation">條形碼標籤位置</param>
        /// <returns>條形碼圖像</returns>
        /// <Author>Sherry</Author>
        /// <CreateDate>2013/01/22</CreateDate>
        /// <RevisionHistory>
        /// <ModifyBy></ModifyBy>
        /// <ModifyDate></ModifyDate>
        /// <ModifyReason></ModifyReason>
        /// </RevisionHistory>
        /// <LastModifyDate></LastModifyDate>
        public  Image GenerateBarCode(string Code, int Width = 300, int Height = 50,
            BarcodeAlignment Alignment = BarcodeAlignment.Center,
            BarcodeLabelLocation LabelLocation = BarcodeLabelLocation.BottomCenter)
        {
            Image BarcodeImage = null;
            try
            {
                //條形碼對齊方式
                switch (Alignment)
                {
                    case BarcodeAlignment.Center:
                        barcode.Alignment = AlignmentPositions.CENTER;
                        break;
                    case BarcodeAlignment.Right:
                        barcode.Alignment = AlignmentPositions.RIGHT;
                        break;
                    default:
                        barcode.Alignment = AlignmentPositions.LEFT;
                        break;
                }
                //條形碼的編碼類型
                BarcodeLib.TYPE EncodeType = TYPE.CODE128;
                //顯示編碼標籤
                barcode.IncludeLabel = true;
                //條形碼旋轉方式
                barcode.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                //條形碼標籤位置
                switch (LabelLocation)
                {
                    case BarcodeLabelLocation.TopLeft:
                        barcode.LabelPosition = LabelPositions.TOPLEFT;
                        break;
                    case BarcodeLabelLocation.TopCenter:
                        barcode.LabelPosition = LabelPositions.TOPCENTER;
                        break;
                    case BarcodeLabelLocation.TopRight:
                        barcode.LabelPosition = LabelPositions.TOPRIGHT;
                        break;
                    case BarcodeLabelLocation.BottomLeft:
                        barcode.LabelPosition = LabelPositions.BOTTOMLEFT;
                        break;
                    case BarcodeLabelLocation.BottomRight:
                        barcode.LabelPosition = LabelPositions.BOTTOMRIGHT;
                        break;
                    default:
                        barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
                        break;
                }
                //字體顏色
                Color FontColor = Color.Black;
                //背景顏色
                Color BackgroundColor = Color.White;

                //生成條形碼圖像
                BarcodeImage = barcode.Encode(EncodeType, Code, FontColor, BackgroundColor, Width, Height);
            }
            catch { }
            //返回條形碼圖像
            return BarcodeImage;
        }
    }
}

