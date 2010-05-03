module Ferly

open System
open Otp
    
Console.Write ":>"  
let mutable input = Console.ReadLine()  

let remoteCall (sql:string) = 
    let cNode = new OtpSelf("querlyclientnode")
    let sNode = new OtpPeer("querly@dmohl-PC")
    let connection = cNode.connect(sNode)
    let arguments = [|new Erlang.String(sql)|] : Erlang.Object[]
    do connection.sendRPC("querly_client", "sql_query", arguments) 
    connection.receiveRPC().ToString()
   
while input <> "quit"  
    do  
    if input <> "quit"   
    then
        let result = remoteCall input  
        Console.Write(result)
        Console.Write "\n:>"   
        input <- Console.ReadLine()

