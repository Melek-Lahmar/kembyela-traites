using System.Collections.Generic;

namespace Kembyela.Helpers
{
    public static class PdfCoordinatesHelper
    {
        // Coordonnées pour formulaire BCT standard (à ajuster selon votre formulaire)
        public static Dictionary<string, FieldCoordinates> GetBctCoordinates()
        {
            return new Dictionary<string, FieldCoordinates>
            {
                // Tous les coordonnées en points (1/72 inch)
                // A4 paysage: 842 x 595 points

                ["Titre"] = new FieldCoordinates(421, 580, 0, 18, TextAlignment.Center),
                ["Timbre"] = new FieldCoordinates(680, 560, 0, 11, TextAlignment.Left),
                ["SectionPaiement"] = new FieldCoordinates(50, 530, 742, 30, TextAlignment.Left),
                ["DateEcheance"] = new FieldCoordinates(50, 490, 120, 0, TextAlignment.Left),
                ["Ville"] = new FieldCoordinates(50, 460, 120, 0, TextAlignment.Left),
                ["DateEdition"] = new FieldCoordinates(50, 430, 120, 0, TextAlignment.Left),
                ["RIB"] = new FieldCoordinates(50, 400, 120, 0, TextAlignment.Left),
                ["Montant"] = new FieldCoordinates(50, 370, 120, 0, TextAlignment.Left),
                ["Monnaie"] = new FieldCoordinates(421, 490, 120, 0, TextAlignment.Left),
                ["OrdreDe"] = new FieldCoordinates(421, 460, 120, 0, TextAlignment.Left),
                ["Payer"] = new FieldCoordinates(421, 430, 120, 0, TextAlignment.Left),
                ["Aval"] = new FieldCoordinates(421, 400, 120, 0, TextAlignment.Left),
                ["Banque"] = new FieldCoordinates(421, 370, 120, 0, TextAlignment.Left),
                ["Protestable"] = new FieldCoordinates(421, 340, 120, 0, TextAlignment.Left),
                ["MontantLettres"] = new FieldCoordinates(50, 280, 742, 40, TextAlignment.Center),
                ["SignatureTire"] = new FieldCoordinates(50, 200, 350, 40, TextAlignment.Left),
                ["SignatureBanque"] = new FieldCoordinates(421, 200, 350, 40, TextAlignment.Left),
                ["SignatureTireur"] = new FieldCoordinates(50, 130, 742, 40, TextAlignment.Left),
                ["BonPourAval"] = new FieldCoordinates(421, 170, 0, 0, TextAlignment.Left),
                ["Numero"] = new FieldCoordinates(680, 30, 0, 0, TextAlignment.Right)
            };
        }

        public class FieldCoordinates
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public TextAlignment Alignment { get; set; }

            public FieldCoordinates(float x, float y, float width, float height, TextAlignment alignment)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
                Alignment = alignment;
            }
        }

        public enum TextAlignment
        {
            Left,
            Center,
            Right
        }
    }
}