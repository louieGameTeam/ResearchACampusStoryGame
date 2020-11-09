// ====================================================================================================            
//            
// Cloud Code for AccountDetailsRequest, write your code here to customize the GameSparks platform.            
//            
// For details of the GameSparks Cloud Code API see https://docs.gamesparks.com/            
//            
// ====================================================================================================            

var levelCount = 9;            

var dates = Spark.getPlayer().getScriptData("OVERRIDES");            
if (dates == null) {            
    dates = [];            
    for (var i = 1; i < levelCount + 1; i++)            
        dates.push("");            
    Spark.getPlayer().setScriptData("OVERRIDES", dates)            
}            
         
var schedule = [];            
for (var i = 1; i < levelCount + 1; i++) {            
    var entryObject = Spark.getGameDataService().getItem("LEVELS", i.toString());            
    if (entryObject.error()) {            
        Spark.setScriptError("ERROR", entryObject.error())            
    } else {            
        var entry = entryObject.document();            
        var data = entry.getData();            
        var date = data.date            
        var ms = Date.parse(data.date);            
        var override = Date.parse(dates[i - 1]);            
        if (!isNaN(override))            
            ms = override;            
        var remaining = ms - Date.now();            
        remaining /= 1000;            
        schedule.push(remaining);  
    }
}            
Spark.setScriptData("SCHEDULE", schedule);