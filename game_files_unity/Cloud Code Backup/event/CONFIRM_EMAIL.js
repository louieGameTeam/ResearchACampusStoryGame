// ====================================================================================================
//
// Cloud Code for CONFIRM_EMAIL, write your code here to customize the GameSparks platform.
//
// For details of the GameSparks Cloud Code API see https://docs.gamesparks.com/
//
// ====================================================================================================


var email = Spark.getData().ADDRESS;

var key = "ThePasswordisPassword";
var data = new Date().getHours() + email;
var token = Spark.getDigester().hmacSha256Base64(key, data);

var contents = "A request has been sent to this email address to set a password in 'Intro to Research - A Campus Story'.\nIf you did not initiate this process, you can ignore this email.\n\nEnter this key in the game to complete the procedure: " + token + "\n\nThis key will remain valid for the remainder of the hour."; 

var myGrid = Spark.sendGrid("introtoresearch", "researchisfun1");
// Spark.getPlayer().setScriptData("SENDGRID", myGrid);
myGrid.addTo( email , "Player" );
myGrid.setFrom("noreply@researchacampusstory.com", "Intro to Research - Support");
myGrid.setSubject("Confirm your email");
myGrid.setText(contents);
var output = myGrid.send();
Spark.getPlayer().setScriptData("SENDGRID", output);