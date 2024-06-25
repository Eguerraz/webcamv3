using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Runtime;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Speech.Synthesis;

namespace webcamv3
{
    public partial class Form1 : Form
    {
        int fram = 0;
        private FilterInfoCollection videoDevices; // Liste des périphériques vidéo disponibles
        private VideoCaptureDevice videoSource; // Périphérique vidéo sélectionné
        private Bitmap image1; // Déclaration de l'image1
        private Bitmap image2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialiser la collection de périphériques vidéo disponibles
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0) // Vérifier s'il y a des périphériques vidéo disponibles
            {
                // Sélectionner le premier périphérique vidéo trouvé
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

                // Définir directement la résolution souhaitée (par exemple, 1280x720)
                SetResolution(videoSource, 640, 480);

                // Abonner à l'événement NewFrame qui se déclenche à chaque nouvelle frame capturée
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);

                // Démarrer la capture vidéo
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("No video sources found");
            }
        }

        private void SetResolution(VideoCaptureDevice videoSource, int width, int height)
        {
            foreach (var capability in videoSource.VideoCapabilities)
            {
                if (capability.FrameSize.Width == width && capability.FrameSize.Height == height)
                {
                    videoSource.VideoResolution = capability;
                    return;
                }
            }

            MessageBox.Show($"La résolution {width}x{height} n'est pas disponible. Utilisation de la résolution par défaut.");
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            fram++;

            if (fram == 30)
            {
                // Cloner l'image capturée pour la traiter
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                // Convertir l'image en termes hexadécimaux
                var (therme, largeur, hauteur) = ImageEnTermesHex(bitmap);
                // Analyser les valeurs hexadécimales
                AnalyserHexValues(therme, largeur, hauteur);
                fram = 0;
            }
        }


        private void AnalyserHexValues(string[,] therme, int largeur, int hauteur)
        {
            int x = 0;
            int y = 0;
            string hex = "";
            int rouge = 0;
            int rhx = 0;
            int rx = 0;
            int R = 0;
            int V = 0;
            int B = 0;
            int verif = 0;
            int validation = 0;
            int rougehautx = 0;
            int rougehauty = 0;
            int rougeinterbasy = 0;
            int rougeinterhauty = 0;
            int milieu = 0;
            int rougeintergeuche = 0;
            int rougeinterdroite = 0;
            int rond = 0;
            int rougenul = 0;
            int noir = 0;
            int milieux = 0;
            //Créer une nouvelle instance de Bitmap pour l'image modifiée
            image1 = new Bitmap(320, 480);

            //afficher limage
            for (y = 0; y < hauteur; y++)
            {

                for (x = 0; x <largeur - 1; x++)
                {

                    hex = therme[x, y];
                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                    B = Convert.ToInt32(hex.Substring(4, 2), 16);

                    // Rouge clair
                    if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50)
                    {
                        rouge++;
                    }

                    else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 100)
                    {
                        rouge++;
                    }

                    else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 100)
                    {
                        rouge++;
                    }

                    // Rouge orangé
                    else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50)
                    {
                        rouge++;
                    }

                    // Rouge brique
                    else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                    {
                        rouge++;
                    }

                    // Rouge rosé
                    else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                    {
                        rouge++;
                    }

                    // Rouge pourpre
                    else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150)
                    {
                        rouge++;
                    }

                    // Rouge violacé
                    else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                    {
                        rouge++;
                    }

                    // Rouge intense
                    else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                    {
                        rouge++;
                    }

                    // Rouge classique
                    else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                    {
                        rouge++;
                    }

                    // Rouge feu
                    else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30)
                    {
                        rouge++;
                    }
                    else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65)
                    {
                        rouge++;
                    }

                    else
                    {
                        rougenul++;
                        //R = 0;
                        //V = 0;
                        //B = 0;

                    }
                    if (rouge < 2 && rougenul > 0)
                    {
                        rouge = 0;
                        rougenul = 0;
                    }
                    if (rouge > 1 && rougenul > 0)
                    {
                        rouge = rouge / 2;

                        if (verif == 0)
                        {
                            rx = 0;

                            rougehauty = y;
                            while (rouge != 0)
                            {

                                hex = therme[rx, y];
                                R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                rx++;
                                rougehautx++;

                                // Rouge clair
                                if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50)
                                {
                                    rouge--;
                                }


                                // Rouge foncé
                                else if (R >= 95 && R <= 150 && V >= 0 && V <= 75 && B >= 0 && B <= 70)
                                {
                                    rouge--;

                                }
                                else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 100)
                                {
                                    rouge--;
                                }

                                // Rouge orangé
                                else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50)
                                {
                                    rouge--;

                                }

                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                                {
                                    rouge--;
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                                {
                                    rouge--;

                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150)
                                {
                                    rouge--;
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                                {
                                    rouge--;
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                                {
                                    rouge--;
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                                {
                                    rouge--;
                                }

                                // Rouge feu
                                else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30)
                                {
                                    rouge--;

                                }
                                else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65)
                                {
                                    rouge--;
                                }
                                verif++;
                            }
                            while (validation < 2)
                            {
                                rougehauty++;
                                if (rougehauty == hauteur - 1 && validation < 2)
                                {
                                    validation = 4;
                                    rouge = 0;
                                    rougenul = 0;
                                    rougehautx = 0;
                                    verif = 0;
                                }
                                else
                                {
                                    rouge = 0;

                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougeinterbasy++;
                                }

                                // Rouge clair
                                if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge foncé
                                else if (R >= 80 && R <= 190 && V >= 0 && V <= 110 && B >= 0 && B <= 125 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 80 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge orangé
                                else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 100 && V > 70 && B > 80)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }

                                // Rouge feu
                                else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }

                                }

                                else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65 && validation == 0)
                                {
                                    rougehauty++;
                                    hex = therme[rx, rougehauty];
                                    R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                    V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                    B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                    rougehauty--;
                                    if (R >= 130 && V > 130 && B > 130)
                                    {
                                        validation++;
                                        rougeinterhauty = rougehauty;
                                    }
                                }


                                // Rouge clair
                                if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }
                                else if (R >= 150 && R <= 170 && V >= 35 && V <= 60 && B >= 35 && B <= 80 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }
                                // Rouge foncé
                                else if (R >= 85 && R <= 150 && V >= 0 && V <= 75 && B >= 0 && B <= 70 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;

                                }


                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;

                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 100 && B <= 150 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }

                                // Rouge feu
                                else if (R >= 250 && V >= 60 && V <= 90 && B >= 0 && B <= 30 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;

                                }

                                else if (R >= 125 && R <= 200 && V >= 5 && V <= 40 && B >= 30 && B <= 65 && validation == 1)
                                {
                                    validation++;
                                    rougeinterbasy = rougehauty;
                                    verif++;
                                }


                            }


                        }

                        if (validation == 2)
                        {
                            validation = 0;
                            milieux = rougeinterhauty + ((rougeinterbasy - rougeinterhauty) / 2);
                            rougeintergeuche = rougehautx;
                            rougeinterdroite = rougehautx;
                            while (validation != 1)
                            {

                                rougeintergeuche--;
                                hex = therme[rougeintergeuche, milieux];
                                R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                B = Convert.ToInt32(hex.Substring(4, 2), 16);

                                // Rouge clair
                                if (R >= 200 && V >= 0 && V <= 50 && B >= 0 && B <= 50)
                                {
                                    validation = 1;
                                }

                                // Rouge foncé
                                else if (R >= 85 && R <= 150 && V >= 0 && V <= 75 && B >= 0 && B <= 70)
                                {
                                    validation = 1;
                                }

                                // Rouge orangé
                                else if (R >= 180 && V >= 50 && V <= 100 && B >= 0 && B <= 50)
                                {
                                    validation = 1;
                                }

                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                                {
                                    validation = 1;
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                                {
                                    validation = 1;
                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 30 && B <= 150)
                                {
                                    validation = 1;
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                                {
                                    validation = 1;
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                                {
                                    validation = 1;
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                                {
                                    validation = 1;
                                }


                                if (rougeintergeuche == 0)
                                {
                                    validation = 1;
                                }
                            }


                            while (validation != 2)
                            {
                                

                                rougeinterdroite++;
                                hex = therme[rougeinterdroite, milieux];
                                R = Convert.ToInt32(hex.Substring(0, 2), 16);
                                V = Convert.ToInt32(hex.Substring(2, 2), 16);
                                B = Convert.ToInt32(hex.Substring(4, 2), 16);
                                // Rouge clair

                                if (rougeinterdroite == largeur - 1)
                                {
                                    validation = 2;
                                }

                                else if (R >= 200 && V >= 35 && V <= 70 && B >= 0 && B <= 60)
                                {
                                    validation++;
                                }

                                // Rouge foncé
                                else if (R >= 85 && R <= 150 && V >= 0 && V <= 75 && B >= 0 && B <= 70)
                                {
                                    validation++;
                                }

                                // Rouge orangé
                                else if (R >= 95 && V >= 50 && V <= 100 && B >= 0 && B <= 60)
                                {
                                    validation++;

                                }

                                // Rouge brique
                                else if (R >= 150 && R <= 200 && V >= 50 && V <= 100 && B >= 50 && B <= 100)
                                {
                                    validation++;
                                }

                                // Rouge rosé
                                else if (R >= 200 && V >= 100 && V <= 150 && B >= 100 && B <= 150)
                                {
                                    validation++;
                                }

                                // Rouge pourpre
                                else if (R >= 150 && R <= 200 && V >= 0 && V <= 50 && B >= 30 && B <= 150)
                                {
                                    validation++;
                                }

                                // Rouge violacé
                                else if (R >= 120 && R <= 170 && V >= 0 && V <= 50 && B >= 120 && B <= 170)
                                {
                                    validation++;
                                }

                                // Rouge intense
                                else if (R >= 220 && V >= 0 && V <= 30 && B >= 0 && B <= 30)
                                {
                                    validation++;
                                }

                                // Rouge classique
                                else if (R >= 200 && R <= 255 && V >= 0 && V <= 65 && B >= 0 && B <= 95)
                                {
                                    validation++;
                                }
                                

                            }
                        }
                        if (rond == 0)
                        {
                            largeur = rougeinterdroite - rougeintergeuche;
                            hauteur = rougeinterbasy - rougeinterhauty;
                            if ((largeur - hauteur) >= 0)
                            {
                                if ((largeur - hauteur) <= (largeur * 5) / 100 || (largeur - hauteur) <= (hauteur * 5) / 100)
                                {
                                    rond++;
                                }
                                else
                                {
                                    rond = 0;
                                    verif = 0;
                                    validation = 0;

                                }

                            }

                            if ((hauteur - largeur) >= 0)
                            {
                                if ((hauteur - largeur) <= (largeur * 5) / 100 || (largeur - hauteur) <= (hauteur * 5) / 100)
                                {
                                    rond++;
                                }
                                else
                                {
                                    rond = 0;
                                    verif = 0;
                                    validation = 0;

                                }

                            }

                        }

                    }
                    if (validation > 2)
                    {
                        validation = 0;
                    }
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, R, V, B);
                    image1.SetPixel(x, y, newColor);

                }



            }
            image2 = new Bitmap(320, 480);
            string[,] panneau = new string[largeur, hauteur];
            x = 0; y = 0;
            for (int py = rougeinterhauty; py < rougeinterbasy; py++)
            {

                for (int px = rougeintergeuche; px < rougeinterdroite; px++)
                {

                    hex = therme[px, py];
                    panneau[x, y] = hex;
                    x++;
                }
                y++;
                x = 0;
            }
            if (largeur > 0 && hauteur > 0 && validation == 2)
            {
                for (int pyy = 0; pyy < hauteur; pyy++)
                {
                    for (int pxx = 0; pxx < largeur; pxx++)
                    {
                        hex = panneau[pxx, pyy];
                        R = Convert.ToInt32(hex.Substring(0, 2), 16);
                        V = Convert.ToInt32(hex.Substring(2, 2), 16);
                        B = Convert.ToInt32(hex.Substring(4, 2), 16);
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(1, R, V, B);
                        image2.SetPixel(pxx, pyy, newColor);
                    }
                }
            }


            int interlarg = 0;
            int interhaut = 0;
            if (validation == 2)
            {
                for (int t = 0; t < 32; t++)
                {

                    noir = 0;
                    int h = (hauteur / 8) * (2 + interhaut);
                    int h2 = (hauteur / 8) * (3 + interhaut);
                    int l = (largeur / 8) * interlarg;
                    int l2 = (largeur / 8) * (1 + interlarg);

                    for (int pyy = h; pyy < h2; pyy++)
                    {
                        for (int pxx = l; pxx < l2; pxx++)
                        {
                            hex = panneau[pxx, pyy];
                            R = Convert.ToInt32(hex.Substring(0, 2), 16);
                            V = Convert.ToInt32(hex.Substring(2, 2), 16);
                            B = Convert.ToInt32(hex.Substring(4, 2), 16);

                            if (R < 100 && V < 100 && B < 100)
                            {
                                noir++;
                            }

                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(1, R, V, B);
                            image2.SetPixel(pxx, pyy, newColor);
                        }
                    }

                    interhaut++;
                    if (interhaut > 3)
                    {
                        interhaut = 0;
                        interlarg++;

                    }
                    if (validation == 2)
                    {
                        int poucentage = 0;
                        if (noir > 0)
                        {
                            poucentage = (noir * 100) / ((hauteur / 8) * (largeur / 8));
                        }
                        if (t == 0)
                        {
                            TBcase1.Text = poucentage.ToString();
                        }
                        else if (t == 1)
                        {
                            TBcase2.Text = poucentage.ToString();
                        }
                        else if (t == 2)
                        {
                            TBcase3.Text = poucentage.ToString();
                        }
                        else if (t == 3)
                        {
                            TBcase4.Text = poucentage.ToString();
                        }
                        else if (t == 4)
                        {
                            TBcase5.Text = poucentage.ToString();
                        }
                        else if (t == 5)
                        {
                            TBcase6.Text = poucentage.ToString();
                        }
                        else if (t == 6)
                        {
                            TBcase7.Text = poucentage.ToString();
                        }
                        else if (t == 7)
                        {
                            TBcase8.Text = poucentage.ToString();
                        }
                        else if (t == 8)
                        {
                            TBcase9.Text = poucentage.ToString();
                        }
                        else if (t == 9)
                        {
                            TBcase10.Text = poucentage.ToString();
                        }
                        else if (t == 10)
                        {
                            TBcase11.Text = poucentage.ToString();
                        }
                        else if (t == 11)
                        {
                            TBcase12.Text = poucentage.ToString();
                        }
                        else if (t == 12)
                        {
                            TBcase13.Text = poucentage.ToString();
                        }
                        else if (t == 13)
                        {
                            TBcase14.Text = poucentage.ToString();
                        }
                        else if (t == 14)
                        {
                            TBcase15.Text = poucentage.ToString();
                        }
                        else if (t == 15)
                        {
                            TBcase16.Text = poucentage.ToString();
                        }
                        else if (t == 16)
                        {
                            TBcase17.Text = poucentage.ToString();
                        }
                        else if (t == 17)
                        {
                            TBcase18.Text = poucentage.ToString();
                        }
                        else if (t == 18)
                        {
                            TBcase19.Text = poucentage.ToString();
                        }
                        else if (t == 19)
                        {
                            TBcase20.Text = poucentage.ToString();
                        }
                        else if (t == 20)
                        {
                            TBcase21.Text = poucentage.ToString();
                        }
                        else if (t == 21)
                        {
                            TBcase22.Text = poucentage.ToString();
                        }
                        else if (t == 22)
                        {
                            TBcase23.Text = poucentage.ToString();
                        }
                        else if (t == 23)
                        {
                            TBcase24.Text = poucentage.ToString();
                        }
                        else if (t == 24)
                        {
                            TBcase25.Text = poucentage.ToString();
                        }
                        else if (t == 25)
                        {
                            TBcase26.Text = poucentage.ToString();
                        }
                        else if (t == 26)
                        {
                            TBcase27.Text = poucentage.ToString();
                        }
                        else if (t == 27)
                        {
                            TBcase28.Text = poucentage.ToString();
                        }
                        else if (t == 28)
                        {
                            TBcase29.Text = poucentage.ToString();
                        }
                        else if (t == 29)
                        {
                            TBcase30.Text = poucentage.ToString();
                        }
                        else if (t == 30)
                        {
                            TBcase31.Text = poucentage.ToString();
                        }
                        else if (t == 31)
                        {
                            TBcase32.Text = poucentage.ToString();
                        }
                    }


                }

            }


            if (validation == 2)
            {

                for (int coupe = 1; coupe < 5; coupe++)
                {

                    for (y = 0; y < hauteur; y++)
                    {
                        System.Drawing.Color colorbalckhaut = System.Drawing.Color.FromArgb(1, 0, 0, 0);
                        image2.SetPixel((largeur / 4) * coupe, y, colorbalckhaut);
                    }
                    for (x = 0; x < largeur; x++)
                    {
                        System.Drawing.Color colorbalckhaut = System.Drawing.Color.FromArgb(1, 0, 0, 0);
                        image2.SetPixel(x, (hauteur / 4) * coupe, colorbalckhaut);
                    }
                }


                int decoupr = 0;
                int newhauteur = (hauteur / 4) * 1;
                int newlargeur = (largeur / 8) * 1;

                for (int coupe = 1; coupe < 5; coupe++)
                {

                    for (y = (hauteur / 4) * 1; y < (hauteur / 4) * 3; y++)
                    {
                        System.Drawing.Color colorbalckhaut = System.Drawing.Color.FromArgb(1, 0, 0, 0);
                        //image2.SetPixel(largeur * coupe, y, colorbalckhaut);
                        image2.SetPixel(newlargeur * (1 + decoupr), y, colorbalckhaut);

                    }
                    for (x = 0; x < largeur; x++)
                    {
                        System.Drawing.Color colorbalckhaut = System.Drawing.Color.FromArgb(1, 0, 0, 0);
                        image2.SetPixel(x, newhauteur + ((newhauteur / 2) * coupe), colorbalckhaut);
                    }
                    decoupr = decoupr + 2;
                }
            }


            pictureBox2.Image = image2;

            if (validation == 2)
            {
                System.Drawing.Color newColor1 = System.Drawing.Color.FromArgb(1, 0, 0, 255);
                image1.SetPixel(rougeintergeuche, milieux, newColor1);
                image1.SetPixel(rougeinterdroite, milieux, newColor1);
                System.Drawing.Color newColor2 = System.Drawing.Color.FromArgb(1, 0, 0, 255);
                image1.SetPixel(rougehautx, milieux, newColor2);
                image1.SetPixel(rougehautx, rougeinterhauty, newColor2);
                image1.SetPixel(rougehautx, rougeinterbasy, newColor2);
            }


            pictureBox1.Image = image1;

            if (validation == 2 && noir > 0)
            {
                int case6 = int.Parse(TBcase6.Text);
                int case7 = int.Parse(TBcase7.Text);
                int case10 = int.Parse(TBcase10.Text);
                int case9 = int.Parse(TBcase9.Text);
                int case11 = int.Parse(TBcase11.Text);
                int case12 = int.Parse(TBcase12.Text);
                int case14 = int.Parse(TBcase14.Text);
                int case5 = int.Parse(TBcase5.Text);
                int case8 = int.Parse(TBcase8.Text);
                int case13 = int.Parse(TBcase13.Text);
                int case15 = int.Parse(TBcase15.Text);
                int case16 = int.Parse(TBcase16.Text);
                if (case6 < 10 && case7 < 10 && case13 < 70)
                {
                    label1.Text = "30";
                }
                else if (case10 < 10 && case9 > 20)
                {
                    label1.Text = "90";
                }
                else if (case5 > case9 && case6 > case10 && case7 > case11 && case8 > case12 && case5 > 70 && case6 > 70 && case7 > 7 && case8 > 70)
                {
                    label1.Text = "80";
                }
                else if (case14 < case10)
                {
                    label1.Text = "50";
                }
                else if (case6 < 10 && case7 < 10 && case13 > 70)
                {
                    label1.Text = "70";
                }
                else if (case9 < 15 && case10 < 15 && case11 < 15 && case12 < 15 && case15 < 15 && case16 < 15)
                {
                    label1.Text = "110";
                }
                else if (case10 < 15 && case11 < 15)
                {
                    label1.Text = "130";
                }

                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.Volume = 100;  // 0...100
                synthesizer.Rate = -2;     // -10...10

                // Synchronous
                synthesizer.Speak(label1.Text + "kilomètre heure");

                // Asynchronous
                synthesizer.SpeakAsync(label1.Text + "kilomètre heure");



            }

        }


        private (string[,], int, int) ImageEnTermesHex(Bitmap bitmap)
        {
            int largeur = bitmap.Width / 2;
            int hauteur = bitmap.Height;
            string[,] therme = new string[largeur, hauteur];

            // Parcourir chaque pixel de l'image
            for (int y = 0; y < hauteur; y++)
            {
                for (int x = largeur; x < bitmap.Width - 1; x++)
                {
                    Color couleurPixel = bitmap.GetPixel(x, y);
                    string hexValue = $"{couleurPixel.R:X2}{couleurPixel.G:X2}{couleurPixel.B:X2}";
                    therme[x - largeur, y] = hexValue;
                }
            }

            return (therme, largeur, hauteur);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Arrêter la capture vidéo proprement lors de la fermeture du formulaire
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
