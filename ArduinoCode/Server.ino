#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include <WiFiUdp.h>

const char* ssid = "LegoTrainBlockNet";
const char* password = "12345678";

int lichtschalter = 0;


ESP8266WebServer server(80);
char incomingPacket[255];

WiFiUDP udp;
IPAddress targetIP;
const unsigned int udpPort = 4210;

void setup() {
  Serial.begin(115200);
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, HIGH);  // LED aus

  WiFi.softAP(ssid, password);
  Serial.println("Server: Access Point gestartet");
  Serial.print("Server: IP-Adresse: ");
  Serial.println(WiFi.softAPIP());

  // Webserver-Endpunkt
  server.on("/", handleRoot);
  server.begin();
  Serial.println("Server: Webserver gestartet");

  udp.begin(udpPort);
  Serial.print("Server: UDP Server gestartet auf Port ");
  Serial.println(udpPort);
}

void loop() {
  server.handleClient();

  int clientCount = WiFi.softAPgetStationNum();
  //digitalWrite(LED_BUILTIN, clientCount > 0 ? LOW : HIGH); // LED an/aus
  digitalWrite(LED_BUILTIN, lichtschalter > 0 ? LOW : HIGH);  // LED an/aus

  handleUDP();

  delay(1000);
}

void handleUDP() {
  int packetSize = udp.parsePacket();
  if (packetSize) {
    int len = udp.read(incomingPacket, 255);
    if (len > 0) {
      incomingPacket[len] = '\0';
    }

    String msgAsString = String(incomingPacket);

    Serial.printf("Server: Empfangen von %s: %s\n", udp.remoteIP().toString().c_str(), incomingPacket);

    const char* prefix = "Licht=";
    if (strncmp(incomingPacket, prefix, strlen(prefix)) == 0) {
      const char* valueStr = incomingPacket + strlen(prefix);
      lichtschalter = atoi(valueStr);
      Serial.printf("Lichtschalter gesetzt auf: %d\n", lichtschalter);
    }
  }
}
void handleRoot() {
  String html = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Verbundene Geräte</title></head><body>";
  html += "<h1>Verbundene Geräte</h1>";
  html += "<ul>";

  struct station_info* station = wifi_softap_get_station_info();
  while (station != NULL) {
    String mac = macToString(station->bssid);
    html += "<li>MAC: " + mac + "</li>";
    station = STAILQ_NEXT(station, next);
  }

  html += "</ul>";
  html += "</body></html>";

  server.send(200, "text/html", html);
}

String macToString(uint8_t* mac) {
  char buf[18];
  sprintf(buf, "%02X:%02X:%02X:%02X:%02X:%02X",
          mac[0], mac[1], mac[2], mac[3], mac[4], mac[5]);
  return String(buf);
}