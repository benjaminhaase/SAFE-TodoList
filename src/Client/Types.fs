module Todo.Types

open Shared
open System

type Visibility = 
  | All
  | Completed
  | NotCompleted 

type State = {
  TodoItems: List<Todo>
  NewTodoDescription: Option<string> 
  Visibility: Visibility
  SortDirection: bool
}

type Msg =
  | LoadTodoItems
  | TodoItemsLoaded of List<Todo>
  | LoadTodoItemsFailure of Exception
  | SetNewTextDescription of string
  | AddTodo
  | AddTodoFailed
  | TodoAdded of Todo
  | DeleteTodo of TodoId
  | DeleteTodoFailure of TodoError
  | ToggleCompleted of TodoId
  | ToggleCompletedFailure of TodoError
  | SetVisibility of Visibility
  | SwitchSort of bool