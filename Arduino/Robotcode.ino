// const int leftforwardpin = 7; // breadboard line 13
// const int leftbackwardpin = 8; // breadboard line 17
// const int rightforwardpin = 13; // breadboard line 14
// const int rightbackwardpin = 12; // breadboard line 16
// const int delaytime = 2000;

// void setup() {
//   pinMode (leftforwardpin, OUTPUT);
//   pinMode (leftbackwardpin, OUTPUT);
//   pinMode (rightforwardpin, OUTPUT);
//   pinMode (rightbackwardpin, OUTPUT);
// }

// void loop(){
//   // stop
//   digitalWrite(leftforwardpin, LOW);
//   digitalWrite(leftbackwardpin, LOW);
//   digitalWrite(rightforwardpin, LOW);
//   digitalWrite(rightbackwardpin, LOW);
//   delay(delaytime);
//   // spin forward
//   digitalWrite(leftforwardpin, HIGH);
//   digitalWrite(leftbackwardpin, LOW
//   digitalWrite(rightforwardpin, HIGH);
//   digitalWrite(rightbackwardpin, LOW);
//   delay(delaytime);
//   //stop
//   digitalWrite(leftforwardpin, HIGH);
//   digitalWrite(leftbackwardpin, HIGH);
//   digitalWrite(rightforwardpin, HIGH);
//   digitalWrite(rightbackwardpin, HIGH);
//   delay(delaytime);
//   // spin backward
//   digitalWrite(leftforwardpin, LOW);
//   digitalWrite(leftbackwardpin, HIGH);
//   digitalWrite(rightforwardpin, LOW);
//   digitalWrite(rightbackwardpin, HIGH);
//   delay(delaytime);
//   // turn right
//   digitalWrite(leftforwardpin, HIGH);
//   digitalWrite(leftbackwardpin, LOW);
//   digitalWrite(rightforwardpin, LOW);
//   digitalWrite(rightbackwardpin, HIGH);
//   delay(delaytime);
//   // turn left
//   digitalWrite(leftforwardpin, LOW);
//   digitalWrite(leftbackwardpin, HIGH);
//   digitalWrite(rightforwardpin, HIGH);
//   digitalWrite(rightbackwardpin, LOW);
//   delay(delaytime);
// }