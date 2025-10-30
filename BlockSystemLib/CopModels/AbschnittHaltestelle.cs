
using System.Collections.Generic;

public class Abschnitt
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public List<Block> Bloecke { get; private set; } = new List<Block>();
    public List<Weiche> Weichen { get; private set; } = new List<Weiche>();

    public required ArduinoController Steuerung { get; set; }
}

public class Block
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public List<int> Nachfolger { get; private set; } = new List<int>();
    public List<int> Vorgaenger { get; private set; } = new List<int>();

    public required Ausfahrtssignal Ausfahrtssignal { get; set; }
    public string? RfidTag { get; set; } // optional für RFID
}


public class Ausfahrtssignal
{
    public required int Id { get; set; }
    public required LedPins LedPins { get; set; }
}

public class LedPins
{
    public required int Rot { get; set; }
    public required int Grun{ get; set; }
    public int? Orange { get; set; }
}

public class Weiche
{
    public class Constants
    {
        public const string gerade = "gerade";
        public const string abzweig = "abzweig";
    }

    public required string Id { get; set; }
    public required string Name { get; set; }
    public required int ServoPin { get; set; }
    public required Dictionary<string, Verbindung> Verbindet { get; set; } 
}

public class ArduinoController
{
    public string Hostname { get; set; }         // z. B. "haltestelle.local"
    public string IpAdresse { get; set; }        // z. B. "192.168.1.42"
    public int UdpPort { get; set; }             // z. B. 4210
    public string Beschreibung { get; set; }     // optional
}

public class Verbindung
{
    public required int Von { get; set; }
    public required int Nach { get; set; }
}

public class Fahrstrasse
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required int StartBlock { get; set; }
    public required int ZielBlock { get; set; }
    public Dictionary<string, string> Weichenstellung { get; private set; } = new Dictionary<string, string>();// z.B. {"W1": "gerade"}
    public required int SignalId { get; set; }
}
