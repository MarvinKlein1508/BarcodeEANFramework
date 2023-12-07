using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BarcodeEANFramework
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class EAN13
    {
        private const string ZEICHENPOOL_LINKS = "ABCDEFGHIJ";
        private const string ZEICHENPOOL_RECHTS = "abcdefghij";
        private int GetPrüfziffer(string ean)
        {
            int prüfziffer = 0;
            int gewichtung = 1; // Gewichtung entweder 1 oder 3

            foreach (var character in ean)
            {
                prüfziffer += gewichtung * Convert.ToInt32(character.ToString());
                gewichtung = 4 - gewichtung;
            }

            prüfziffer %= 10;


            prüfziffer = (10 - prüfziffer) % 10;


            return prüfziffer;
        }

        private string ShiftCharacter(string ean, int p1, int p2, int p3)
        {

            int z;
            StringBuilder sb = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                z = ean[i];

                if (i == p1 || i == p2 || i == p3)
                {
                    z += 11;
                }

                sb.Append((char)z);
            }

            return sb.ToString();
        }

        public string GetEAN13BarcodeString(string ean)
        {
            if (ean is null || ean.Length != 12)
            {
                return string.Empty;
            }

            if (!ean.All(Char.IsDigit))
            {
                return string.Empty;
            }

            string links = string.Empty;
            string rechts = string.Empty;


            int z = 0;

            for (int i = 1; i < 7; i++)
            {
                z = Convert.ToInt32(ean[i].ToString());
                links += ZEICHENPOOL_LINKS[z];
            }


            z = Convert.ToInt32(ean[0].ToString());
            if (z == 1)
            {
                links = ShiftCharacter(links, 2, 4, 5);
            }
            else if (z == 2)
            {
                links = ShiftCharacter(links, 2, 3, 5);
            }
            else if (z == 3)
            {
                links = ShiftCharacter(links, 2, 3, 4);
            }
            else if (z == 4)
            {
                links = ShiftCharacter(links, 1, 4, 5);
            }
            else if (z == 5)
            {
                links = ShiftCharacter(links, 1, 2, 5);
            }
            else if (z == 6)
            {
                links = ShiftCharacter(links, 1, 2, 3);
            }
            else if (z == 7)
            {
                links = ShiftCharacter(links, 1, 3, 5);
            }
            else if (z == 8)
            {
                links = ShiftCharacter(links, 1, 3, 4);
            }
            else if (z == 9)
            {
                links = ShiftCharacter(links, 1, 2, 4);
            }
            else
            {
                // Nichts notwendig
            }

            for (int i = 7; i < 13; i++)
            {
                if (i != 12)
                {
                    z = Convert.ToInt32(ean[i].ToString());
                }
                else
                {
                    z = GetPrüfziffer(ean);

                }

                rechts += ZEICHENPOOL_RECHTS[z];
            }

            string barcode = $"{ean[0]}<{links}={rechts}>";

            return barcode;
        }
    }
}
