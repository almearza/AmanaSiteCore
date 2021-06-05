import { Component, Input, OnInit } from "@angular/core";
import { FormControl } from "@angular/forms";
import { EditorConfig } from "src/app/_models/txtconfig";

@Component({
  selector: "rich-text-editor",
  templateUrl: "./rich-text-editor.component.html",
  styleUrls: ["./rich-text-editor.component.css"]
})
export class RichTextEditorComponent implements OnInit {
  quillConfiguration = EditorConfig;
  @Input() control: FormControl;
  constructor() {}

  ngOnInit() {
    this.control = this.control ?? new FormControl();
  }
}