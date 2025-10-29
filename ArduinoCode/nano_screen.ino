#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET    -1
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

void setup() {
  Serial.begin(9600);

  if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    Serial.println(F("Display nicht gefunden"));
    for (;;);
  }

  display.clearDisplay();
  display.setTextSize(1);
  display.setTextColor(SSD1306_WHITE);
  display.setCursor(0, 0);
  display.println("Hallo Florian!");
  display.display();
}

void loop() {
  digitalWrite(LED_BUILTIN, LOW);  // LED aus
Serial.println("aus");
   display.println("aus");
   display.display();
   delay(1000);
   digitalWrite(LED_BUILTIN, HIGH);  // LED aus   
   display.println("an");
   display.display();
   Serial.println("an");
   delay(1000);
}