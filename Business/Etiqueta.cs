﻿using System;



using System.Drawing;
using System.Drawing.Printing;
using System.Collections;

namespace Business
{


    public class Etiqueta
    {
        ArrayList headerLines = new ArrayList();
        ArrayList subHeaderLines = new ArrayList();
        ArrayList subHeaderLinesNegrita = new ArrayList();
        ArrayList subCodigoBarras = new ArrayList();
        ArrayList totales = new ArrayList();
        ArrayList footerLines = new ArrayList();
        private Image headerImage = null;

        int count = 1;

        int maxChar = 35;
        int maxCharDescription = 20;

        int imageHeight = 0;
        
        float leftMargin = 20;///estaba en 14
        float topMargin = 2;

        string fontName = "Arial";
        
        int fontSize =7;
        int fontSizeSubLinea = 7;
        int fontSizeCodigoBarras = 12;
        
        Font printFont = null;
        Font printFontSubLinea = null;
        Font printFontCodigoBarras = null;
        SolidBrush myBrush = new SolidBrush(Color.Black);

        Graphics gfx = null;

        string line = null;

       
        public Image HeaderImage
        {
            get { return headerImage; }
            set { if (headerImage != value) headerImage = value; }
        }

        public int MaxChar
        {
            get { return maxChar; }
            set { if (value != maxChar) maxChar = value; }
        }

        public int MaxCharDescription
        {
            get { return maxCharDescription; }
            set { if (value != maxCharDescription) maxCharDescription = value; }
        }

        public int FontSize
        {
            get { return fontSize; }
            set { if (value != fontSize) fontSize = value; }
        }

        public int FontSizeCodigoBarras
        {
            get { return fontSizeCodigoBarras; }
            set { if (value != fontSizeCodigoBarras) fontSizeCodigoBarras = value; }
        }
        public string FontName
        {
            get { return fontName; }
            set { if (value != fontName) fontName = value; }
        }

        public void AddHeaderLine(string line)
        {
            headerLines.Add(line);
        }

        public void AddSubHeaderLine(string line)
        {
            subHeaderLines.Add(line);
        }
        public void AddSubHeaderLineNegrita(string line)
        {
            subHeaderLinesNegrita.Add(line.ToUpper());
        }
        public void AddCodigoBarras(string line, string fuenteBarCode)
        {
            switch (fuenteBarCode)
            {
                case "CCode39": line = "*" + line + "*"; break;
                case "Ccode39M43":
                    {
                        string cVerificador = ValidateMod43(line);
                     line = "*" + line  + cVerificador + "*"; break;
                }
            }
            subCodigoBarras.Add(line);
        }


        //public void AddItem(string cantidad, string item, string price)
        //{
        //    OrderItem newItem = new OrderItem('?');
        //    items.Add(newItem.GenerateItem(cantidad, item, price));
        //}

        public void AddTotal(string name, string price)
        {
            OrderTotal newTotal = new OrderTotal('?');
            totales.Add(newTotal.GenerateTotal(name, price));
        }

        public void AddFooterLine(string line)
        {
            footerLines.Add(line);
        }

        private string AlignRightText(int lenght)
        {
            string espacios = "";
            int spaces = maxChar - lenght;
            for (int x = 0; x < spaces; x++)
                espacios += " ";
            return espacios;
        }

        private string DottedLine()
        {
            string dotted = "";
            for (int x = 0; x < maxChar; x++)
                dotted += "=";
            return dotted;
        }

        public bool PrinterExists(string impresora)
        {
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                if (impresora == strPrinter)
                    return true;
            }
            return false;
        }

        public void PrintTicket(string impresora, string fuenteBarCode)
        {
            printFont = new Font(fontName, fontSize, FontStyle.Bold);
            fuenteBarCode="CCode39";
            printFontCodigoBarras = new Font(fuenteBarCode, fontSizeCodigoBarras, FontStyle.Regular);
            printFontSubLinea = new Font(fontName, fontSizeSubLinea, FontStyle.Regular);
            PrintDocument pr = new PrintDocument();
            pr.PrinterSettings.PrinterName = impresora;
            pr.PrintPage += new PrintPageEventHandler(pr_PrintPage);
            pr.Print();
        }

        private string ValidateMod43(string barcode)
        {
            int subtotal = 0;
            const string charSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";

            foreach (char c in barcode)

                subtotal += charSet.IndexOf(c);

            return charSet[subtotal % 43].ToString();

        }

        private void pr_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            gfx = e.Graphics;

        //    DrawImage();
            DrawHeader();
            DrawSubHeader();
            DrawSubHeaderNegrita();
            DrawCodigoBarras();
                      //DrawItems();
           // DrawTotales();
            DrawFooter();

            //if (headerImage != null)
            //{
            //    HeaderImage.Dispose();
            //    headerImage.Dispose();
            //}
        }

        private void DrawSubHeaderNegrita()
        {
            foreach (string header in subHeaderLinesNegrita)
            {
                Font printFont_Area= new Font(fontName, 7, FontStyle.Bold);
                line = header;
                gfx.DrawString(line, printFont_Area, myBrush, leftMargin, YPosition(topMargin), new StringFormat());

                count++;
                //  }
            }
            //DrawEspacio();
        }

        private float YPosition(float topMargin)
        {
            return topMargin + (count *  printFontSubLinea.GetHeight(gfx) + imageHeight);
            if (topMargin==float.Parse("-1")) return 0;
            //return topMargin + (count * float.Parse("8.5") + imageHeight);
        }

        private void DrawImage()
        {
            if (headerImage != null)
            {
                try
                {
                    gfx.DrawImage(headerImage, new Point((int)leftMargin, (int)YPosition(1)));
                    double height = ((double)headerImage.Height / 58) * 15;
                    imageHeight = (int)Math.Round(height) + 3;
                }
                catch (Exception)
                {
                }
            }
        }

        private void DrawHeader()
        {
            foreach (string header in headerLines)
            {                
                {
                    if (header.Length > 20) line = header.Substring(0,19);
                    else                    line = header;

                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(topMargin), new StringFormat());

                    count++;
                }
            }
         //   DrawEspacio();
        }


        private void DrawSubHeader()
        {
            foreach (string header in subHeaderLines)
            {

                if (header.Length > 35) line = header.Substring(0, 34);
                else line = header;

                    gfx.DrawString(line, printFontSubLinea, myBrush, leftMargin, YPosition(topMargin), new StringFormat());

                    count++;
              //  }
            }
            //DrawEspacio();
        }

        private void DrawCodigoBarras()
        {
            foreach (string subHeader in subCodigoBarras)
            {
                
                    line = subHeader;

                    gfx.DrawString(line, printFontCodigoBarras, myBrush, leftMargin, YPosition(2), new StringFormat());

                    count++;
                
            }
            //DrawEspacio();
        }

        


     
        private void DrawFooter()
        {
            foreach (string footer in footerLines)
            {
                
                    line = footer;
                    Font printFont_Numero = new Font("Arial", 10, FontStyle.Underline );
                
                
                    gfx.DrawString(line, printFont_Numero, myBrush, leftMargin+32, 5, new StringFormat(StringFormatFlags.DirectionVertical));
                    
                    count++;
              
            }
       //     leftMargin = 0;
           // DrawEspacio();
        }

        private void DrawEspacio()
        {
            line = "";
            Font printFontLineaEspacio = new Font(fontName, 2, FontStyle.Regular);
            gfx.DrawString(line, printFontLineaEspacio, myBrush, leftMargin, YPosition(1), new StringFormat());

            count++;
        }

       
    }

 
    public class OrderTotal
    {
        char[] delimitador = new char[] { '?' };

        public OrderTotal(char delimit)
        {
            delimitador = new char[] { delimit };
        }

        public string GetTotalName(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return delimitado[0];
        }

        public string GetTotalCantidad(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return delimitado[1];
        }

        public string GenerateTotal(string totalName, string price)
        {
            return totalName + delimitador[0] + price;
        }
    }
}