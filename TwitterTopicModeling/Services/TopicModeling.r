#https://stackoverflow.com/questions/18224439/run-r-script-with-start-process-in-net
#https://stackoverflow.com/questions/18345459/execute-local-r-script-via-r-net

library(jsonlite)
all.equal(mtcars, fromJSON(toJSON(mtcars)))

{
  "r.alwaysUseActiveTerminal": true,
  "r.bracketedPaste": true,
  "r.sessionWatcher": true
}

{
  "[r]": {
    "editor.formatOnSave": false
  }
}


