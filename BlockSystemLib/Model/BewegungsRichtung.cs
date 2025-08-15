

namespace BlockSystemLib.Model
{
    public class BewegungsRichtungTyp
    {
        public const int UNBEKANNT = 0;
        public const int VORWÄRTS = 1;
        public const int RÜCKWÄRTS = 2;
        public const int STOP = 3;
    }    

    public class BewegungsRichtung
    {
        public int RichtungTyp = BewegungsRichtungTyp.UNBEKANNT;

        public override string ToString()
        {            
            switch(RichtungTyp)
            {
                case BewegungsRichtungTyp.STOP:
                    return "Stop";
                case BewegungsRichtungTyp.RÜCKWÄRTS:
                    return "Rückwärts";
                case BewegungsRichtungTyp.UNBEKANNT:
                    return "Unbekannt";
                case BewegungsRichtungTyp.VORWÄRTS:
                    return "Vorwärts";
                default:
                    return "Fehler";
            }

        }        
    }
}
