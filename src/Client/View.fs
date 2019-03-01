module Todo.View

open Shared
open Todo.Types
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

let divider =  span [ Style [ MarginLeft 5; MarginRight 5 ] ] [ ]

let renderTodo (item: Todo) dispatch = 
    let toggleText = if item.Completed then "Actually, Not Yet!" else "Complete"
    let dispatchToggle = OnClick (fun _ -> dispatch (ToggleCompleted item.Id))
    let dispatchDelete = OnClick (fun _ -> dispatch (DeleteTodo item.Id))
    
    let todoStyle = 
      match item.Completed with
      | true ->  Style [ Color "red"; FontSize 19; Padding 5; TextDecoration "line-through"]
      | false ->  Style [ Color "green"; FontSize 19; Padding 5 ]

    div 
      [ ] 
      [ p [ todoStyle ] [ [ item.Description; item.DateAdded.ToString("dd-MM-yyyy HH:mm:ss") ] |> String.concat " " |> str ]
        button [ ClassName "button is-info"; dispatchToggle ] [ str toggleText ]
        divider
        button [ ClassName "button is-danger"; dispatchDelete ] [ str "Delete" ] ]

let int2string x = sprintf "%i" x    


let addTodo (state: State) dispatch = 
  let textValue = defaultArg state.NewTodoDescription ""
  div 
    [ ClassName "field has-addons"; Style [Padding 5; Width 800] ] 
    [ div 
        [ ClassName "control is-large" ]
        [ input [ ClassName "input is-large"
                  Placeholder "Add Todo"
                  DefaultValue textValue
                  Value textValue
                  OnChange (fun ev -> dispatch (SetNewTextDescription (!!ev.target?value)))] ] 
      div 
        [ ClassName "control is-large" ]
        [ button [ ClassName "button is-primary is-large"; OnClick (fun _ -> dispatch AddTodo) ] [ str "Add Todo" ] ] 
      div
        [ ClassName "control is-large" ]
        [ button [ ClassName "button is-primary is-large"; OnClick (fun _ -> dispatch (SwitchSort state.SortDirection))] [ str "Switch Sort" ] ] ] 
 
let render  (state: State) dispatch = 
    let sortedTodos sortDirection = 
      if sortDirection then
        state.TodoItems 
        |> List.sortBy (fun todo -> todo.DateAdded) 
        |> List.map (fun todo -> renderTodo todo dispatch)
      else
        state.TodoItems 
        |> List.sortBy (fun todo -> todo.DateAdded) 
        |> List.rev
        |> List.map (fun todo -> renderTodo todo dispatch)

    div 
     [ Style [ Padding 20 ] ]
     [ yield h1 [ Style [ FontSize 24 ] ] [ str "SAFE Todo-List" ]
       yield hr [ ]
       yield addTodo state dispatch
       yield! sortedTodos state.SortDirection ]