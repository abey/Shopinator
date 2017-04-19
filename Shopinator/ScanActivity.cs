using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ScanditBarcodePicker.Android;
using ScanditBarcodePicker.Android.Recognition;

namespace Shopinator
{
    [Activity(Label = "ScanActivity")]

    public class ScanActivity : Activity, IOnScanListener, IDialogInterfaceOnCancelListener
    {
        private BarcodePicker picker;
        int resultData;
        public static string appKey = "AYe56jITKMMJB4mjUzUvieos2qlrNovm5EMhejZ6Z365REbbrmcstoo38ZKjfUqsjl8Iu4EJ3/sCF5UhN2dHzt8+qdMgfHBRb1PxUHp+zFCPXmmSD2qhOAkqX9mqGLp0wAdxbBWk2FVYPqXlgmFeny07pFvAuMuFV/13w6fXKuUM6oBYFloziGEqN1ovmifbD16BSgvWKIu2qJ6dA/lEyOQLLbjx3OEcrolPRwn2wdSelIECIE8+WdiYOlgcBGcFBdRyrvbh5UTS/20pDO9Qi48wTwXqKzcn8t2KSZokFnzqwjM3W4wLJEfy1EW6Anm4zNA+rhS6BFCDmEtHbXr/Lx9j9LfgB0ah02lO3/y7SBnMtTr7ozaRm5qLZYyJv2VtAgOgJPr7T9YE7QgBkUntNsXoQBW90la47+NPixNVkv3ODDbQ0GOTDiWvtU0X7rB6N1oBsvvGO1XlfjB7FnPEN7Kd6o4eNPW7Ksos0VYF/XR4BR3CqhACcTmPrLHTzG5pZG7NvmrCA0IdWNiLvs1/Hqi3O/uYnlmuZaBJaTqIUNf78VB1OqVh2LykPc0OkmHSkkyPG4w5saX1vicEV2GmBsdS2an38uE7lMM2+98jiEyQ6gLbd7SHRb2NqL1CyAj8dBd5WYTwi/+8ODFIJQrUzgGi5EypZF5NkGGzkdZ/8LBYc6Aq9RK1OA8GwkR9ccVqwIn2lpf/d9cOvjo1BVxsxe68ehKav7CplLtWLxaXyYIvy0VpOI9iNsUV6vAAfWo53nBkSgUsOpW39pOp7kYkIKbk0/3LaxIGh6IU+BH/k/0=";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            ScanditLicense.AppKey = appKey;
            ScanSettings settings = ScanSettings.Create();
            int[] symbologiesToEnable = new int[] {
                Barcode.SymbologyEan13,
                Barcode.SymbologyEan8,
                Barcode.SymbologyUpca,
                Barcode.SymbologyDataMatrix,
                Barcode.SymbologyQr,
                Barcode.SymbologyCode39,
                Barcode.SymbologyCode128,
                Barcode.SymbologyInterleaved2Of5,
                Barcode.SymbologyUpce
            };

            for (int sym = 0; sym < symbologiesToEnable.Length; sym++)
            {
                settings.SetSymbologyEnabled(symbologiesToEnable[sym], true);
            }

            SymbologySettings symSettings = settings.GetSymbologySettings(Barcode.SymbologyCode128);
            short[] activeSymbolCounts = new short[] {
                7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
            };
            symSettings.SetActiveSymbolCounts(activeSymbolCounts);
            // For details on defaults and how to calculate the symbol counts for each symbology, take
            // a look at http://docs.scandit.com/stable/c_api/symbologies.html.

            picker = new BarcodePicker(this, settings);
            picker.SetOnScanListener(this);
            SetContentView(picker);
        }

        public void DidScan(IScanSession session)
        {
            if (session.NewlyRecognizedCodes.Count > 0)
            {
                Barcode code = session.NewlyRecognizedCodes[0];
                Console.WriteLine("barcode scanned: {0}, '{1}'", code.SymbologyName, code.Data);
                GC.Collect();
                session.StopScanning();
                resultData = Int32.Parse(code.Data);
                string dataLink = "https://abinodh.github.io/Shopinator/" + resultData + ".png";
                var uri = Android.Net.Uri.Parse(dataLink);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            }
        }

        public void OnCancel(IDialogInterface dialog)
        {
            picker.StartScanning();
        }

        protected override void OnResume()
        {
            picker.StartScanning();
            base.OnResume();
        }

        protected override void OnPause()
        {
            GC.Collect();
            picker.StopScanning();
            base.OnPause();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Finish();
        }
    }
}
