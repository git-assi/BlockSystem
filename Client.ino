#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

const char* ssid = "ESP8266_AP";
const char* password = "12345678";

WiFiUDP udp;
IPAddress targetIP;
const unsigned int udpPort = 4210;
int i = 0;

#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET    -1
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

void setup() {
   Serial.begin(115200);
  delay(10);

  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, HIGH);  // LED aus

    display.clearDisplay();
  display.setTextSize(1);
  display.setTextColor(SSD1306_WHITE);
  display.setCursor(0, 0);
  display.println("Hallo Welt!");
  display.display();

  Serial.println();
  Serial.println("Client: Verbindung mit WLAN wird hergestellt...");
  WiFi.begin(ssid, password);

   if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    Serial.println(F("Display nicht gefunden"));
    for (;;);
  }



  // Warten bis verbunden
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
    if (i == 0) {
      i = 1;
    } else {
      i = 0;
    }
    digitalWrite(LED_BUILTIN, i == 1 ? LOW : HIGH);
  }


  Serial.println("Client: WLAN verbunden!");
  IPAddress localIP = WiFi.localIP();
  Serial.print("Lokale IP: ");
  Serial.println(localIP);

  // Ziel-IP berechnen: letztes Oktett durch 1 ersetzen
  targetIP = IPAddress(localIP[0], localIP[1], localIP[2], 1);
  Serial.print("Client: Ziel-IP für UDP: ");
  Serial.println(targetIP);

  digitalWrite(LED_BUILTIN, LOW);

  const char* message = "Hallo von NodeMCU!";
  udp.beginPacket(targetIP, udpPort);
  udp.write(message);
  udp.endPacket();
}

void loop() {

  char* message = "";
  if (i == 0) {
    
    char* message = "Licht=0";
    udp.beginPacket(targetIP, udpPort);
    udp.write(message);
    udp.endPacket();
    i = 1;
  } else {
    char* message = "Licht=1";
    udp.beginPacket(targetIP, udpPort);
    udp.write(message);
    udp.endPacket();
    i = 0;
  }


  Serial.println("Client: UDP-Nachricht gesendet.");
  delay(5000);
}
