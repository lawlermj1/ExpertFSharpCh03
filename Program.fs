// Learn more about F# at http://fsharp.org

open System 
open System.IO
open System.Net

//  standard sample recursion function 
let rec factorial n = if n <= 1 then 1 else n * factorial (n - 1)  

//  recursive length 
let rec length l = 
    match l with 
    | [] -> 0
    | h :: t -> 1 + length t  

//  tests if Upper case 
let isUpper (x : char) = (System.Char.ToUpper x) = x

//  converts letter to int 
let let2nat (letter : char) = int letter - int 'a' 

// Get the contents of the URL via a web request 
let http (url: string) =
    let req = WebRequest.Create(url) 
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

//  fetch from web page 
let fetch url = 
    try Some (http url) 
    with :? System.Net.WebException -> None  

//  example fetch some text from a web page 
match (fetch "https://www.nature.com") with 
// match (fetch "https://www.news.com.au/" with 
    | Some text -> printfn "text - %s" text.[..200]  
    | None -> printfn "**** no web page found"  

//  example of patern matching 
//  This is a list example. How to define for Array or Seq? 
let printFirst xs =
    match xs with 
    | h :: _ -> printfn "The first item in the list is %A" h 
    | [] -> printfn "No items in the list"  

//  get sign of an int  
let sign x =
    match x with 
    | _ when x < 0 -> -1 
    | _ when x > 0 -> 1 
    | _ -> 0 

//  extract from a triple
let fst3 (a, _, _) = a
let snd3 (_, b, _) = b
let thd3 (_, _, c) = c

//  timer performance function
let time f = 
//  need full module path 
    let start = System.DateTime.Now 
//  using () makes it strict - otherwise it will be lazy and not execute 
    let res = f()
    let finish = System.DateTime.Now 
    (res, finish - start)  

//  some example data 
let genList x = [1..x ]  


let delimiters = [| ' '; '\n'; '\t'; '<'; '>'; '=' |] ;;

let getWords (s: string) = s.Split delimiters  

let getStats site = 
    let url = "http://" + site  
    let html = http url 
    let hwords = html |> getWords 
    let hrefs = html |> getWords |> Array.filter (fun s -> s = "href")    
    (site, html.Length, hwords.Length, hrefs.Length)

[<EntryPoint>]
let main argv =
    let webTime = time (fun () -> http "https://www.nature.com")  
    printfn "webTime = %A" webTime 
    printfn "factorial 10  = %d" (factorial 10) 
    let letterList = (['a'..'z'] @ ['A'..'Z'] @['0'..'9'])  
//  let letterArray = List.toArray letterList ;;
    printfn "length letterList = %d" (length letterList) 
    printfn "List.head letterList = %A" (List.head letterList) 
    printfn "List.tail letterList = %A" (List.tail letterList) 
    printfn "List.append letterList = %A" (List.append letterList) 
    printfn "List.filter isUpper letterList = %A" (List.filter isUpper letterList) 
    printfn "List.map let2nat letterList = %A" (List.map let2nat letterList) 
    printfn "List.zip letterList = %A" (List.zip letterList (List.map let2nat letterList)) 
    printfn "List.toArray letterList = %A" (List.toArray letterList) 
    printfn "All finished from ExpertF#Ch03" 
    0 // return an integer exit code
