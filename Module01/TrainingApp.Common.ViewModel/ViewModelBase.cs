using System.Collections.Generic;

namespace TrainingApp.Common.ViewModel
{
    public class ViewModelBase
    {
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }
        public bool IsEntityValid { get; set; }
        public string Mode { get; set; }

        public bool IsDetailAreaVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsSearchAreaVisible { get; set; }

        public List<KeyValuePair<string, string>> ValidationErrors { get; protected set; }

        public ViewModelBase()
        {
            Init();
        }

        protected virtual void Get() {
            ListMode();
        }
        protected virtual void ResetSearch() { }
        protected virtual void Add() {
            AddMode();
        }
        protected virtual void Edit() {
            IsEntityValid = true;
            EditMode();
        }
        protected virtual void Delete() {
            Get();
        }
        protected virtual void Save() {            
            if (ValidationErrors.Count > 0) {
                IsEntityValid = false;
            }

            if (!IsEntityValid) {
                if (Mode == "Add") { AddMode(); }
                if (Mode == "Edit") { EditMode(); }
            }
        }

        protected virtual void Init()
        {            
            ValidationErrors = new List<KeyValuePair<string, string>>();
            EventCommand = "List";
            EventArgument = "";            
        }

        protected virtual void ListMode()
        {
            IsDetailAreaVisible = false;
            IsListAreaVisible = true;
            IsSearchAreaVisible = true;

            Mode = "List";
        }
        protected virtual void AddMode()
        {
            IsDetailAreaVisible = true;
            IsListAreaVisible = false;
            IsSearchAreaVisible = false;

            Mode = "Add";
        }
        protected virtual void EditMode()
        {
            IsDetailAreaVisible = true;
            IsListAreaVisible = false;
            IsSearchAreaVisible = false;

            Mode = "Edit";
        }

        public virtual void HandleRequest()
        {
            switch (EventCommand.ToLower()) {
                case "list":
                case "search":
                    Get();
                    break;

                case "resetsearch":
                    ResetSearch();
                    Get();
                    break;

                case "add":
                    Add();
                    break;

                case "save":
                    Save();
                    if (IsEntityValid) {
                        Get();
                    }
                    break;

                case "edit":
                    Edit();
                    break;

                case "delete":
                    Delete();
                    break;

                case "cancel":
                    Get();
                    break;

                default:
                    Get();
                    break;
            }
        }
    }
}
