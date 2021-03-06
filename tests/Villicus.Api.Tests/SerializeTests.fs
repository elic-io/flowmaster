﻿module SerializeTests

open Xunit
open Villicus.Domain
open Villicus.Serialization
#if FABLE_COMPILER
open Thoth.Json
#else
open Thoth.Json.Net
#endif

[<Fact>]
let ``round trip VersionedWorkflowId`` () =
    let ver = { Id = WorkflowId System.Guid.Empty; Version = Version 0UL }
    ver |> VersionedWorkflowId.Encoder |> Encode.toString 4
    |> Decode.fromString VersionedWorkflowId.Decoder
    |> Result.inject(fun verResult -> Assert.Equal (ver, verResult))
    |> Result.injectError (fun e -> e |> exn |> raise)
    |> ignore

[<Fact>]
let ``round trip WorkflowId`` () =
    let wid = System.Guid.NewGuid() |> WorkflowId
    wid |> WorkflowId.Encoder |> WorkflowId.Decoder ""
    |> Result.inject (fun widResult -> Assert.Equal (wid, widResult))
    |> Result.injectError (fun (msg,_) -> msg |> exn |> raise)