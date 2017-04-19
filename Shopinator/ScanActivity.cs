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
        public static string appKey = "AVwpuQYTHE5gFCB1uhW2ICsgS+avHh+YAlLHBKxsncBaRoV3xVR3IYJmaaGYQi9sm23PdCUmuHr/PGkVWHRzoDFRazqWWhFnqiElo45yqH+4Q0b4yQSwZ/J+vx7ihKaJcf0av8FOne80mD8oiknPBMQSwbPPtL3UrCaASyxDwEsNn/q0xNFSYLlNvcSRfVjM7FYTQrffmhzHIR1Ogmy+zDlZfJVROEiNwWdVeKsMLpnp38akpUE7htMQBL6ncZa+9pf4Avx5jlh/KyquxHuz0wwYnBWp4MSFPk6opudlpJK+UFt0ga1aMzHsS1t0g396ev3LVevLoT8S43CgmI4pLmMBwR+LCY9Dr6fOf5yRM2lb1wGzlE9PczoegYt7fcZ3v26lTVyJF+pGcVuLn4k4OUfY6CELsMx3PK7EFd4YVB8vgZWoE6H6xn6G8g6R8bHRK72y664RijgvGxIDDKqE02yp9wHtLOUr8qxwsBGIFdK34RaDqZJJ+sMI2rRWmft7o47V0Duaegx1gcn4AAd1NCVdvqITbeX9SD91uMXGtvAufgRCaWnCnY5GPo2rlgUc1nUf/KKvCaqUDp62ZNTsiPrNHWwdC96mUD1WJwEPM9xtBcsaY2vQE9YJpPEk3FaBKcMs5/QnvdCtQ1qPNx5En6Dg6gi/WTdCpyUXTx3Re0pk+gFzP4WFBXV1QqC4DhpjhWs8ZbHqfvWoOL4iC0at33bwJq7u6iDIWCrqquXzEHGB1vkSqwb8XIf5AXsTX15zLMTnghe6sjXPnO+XXtflgQ06iBMFDwDoYw==";

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

                RunOnUiThread(() => {
                    AlertDialog alert = new AlertDialog.Builder(this)
                        .SetTitle(code.SymbologyName + " Barcode Detected")
                        .SetMessage(code.Data)
                        .SetPositiveButton("OK", delegate {
                            picker.StartScanning();
                        })
                        .SetOnCancelListener(this)
                        .Create();

                    alert.Show();
                });
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
