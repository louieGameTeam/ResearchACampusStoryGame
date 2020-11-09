// ====================================================================================================
//
// Cloud Code for SAVE, write your code here to customize the GameSparks platform.
//
// For details of the GameSparks Cloud Code API see https://docs.gamesparks.com/
//
// ====================================================================================================

var DAT = Spark.getData().DAT;
var PROGRESS = Spark.getData().PROGRESS;

Spark.getPlayer().setScriptData("DAT", DAT);
Spark.getPlayer().setScriptData("PROGRESS", PROGRESS);