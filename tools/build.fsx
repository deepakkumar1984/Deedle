﻿// --------------------------------------------------------------------------------------
// Builds the documentation from FSX files in the 'samples' directory
// (the documentation is stored in the 'docs' directory)
// --------------------------------------------------------------------------------------

#I "../packages/FSharp.Formatting.1.0.15/lib/net40"
#load "../packages/FSharp.Formatting.1.0.15/literate/literate.fsx"
open System.IO
open FSharp.Literate

let (++) a b = Path.Combine(a, b)
let template = __SOURCE_DIRECTORY__ ++ "template.html"
let sources  = __SOURCE_DIRECTORY__ ++ "../samples"
let output   = __SOURCE_DIRECTORY__ ++ "../docs"

// Root URL for the generated HTML
//let root = "http://fsharp.github.com/FSharp.Data"

// When running locally, you can use your path
let root = @"file://C:\dev\FSharp.DataFrame\docs"


// Copy all sample data files to the "data" directory
let sourceDocs = sources ++ "data"
let outputDocs = output ++ "data"

if Directory.Exists outputDocs then Directory.Delete(outputDocs, true)
Directory.CreateDirectory outputDocs
for fileInfo in DirectoryInfo(sourceDocs).EnumerateFiles() do
    fileInfo.CopyTo(outputDocs ++ fileInfo.Name) |> ignore

// Generate HTML from all FSX files in samples & subdirectories
let build () =
  for sub in [ "." ] do
    Literate.ProcessDirectory
      ( sources ++ sub, template, output ++ sub, 
        replacements = [ "root", root ] )

build()