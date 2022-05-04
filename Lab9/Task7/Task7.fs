open System
open System.Windows.Forms

let onTextChanged (textBox:TextBox) (progressBar:ProgressBar) eventArgs =
    progressBar.Value <- Math.Min(Math.Max(textBox.Text.Trim().Length * 10, 0), 100)

let initForm() =
    let form = new Form(Width= 400, Height = 300, Text = "F# MainForm")
    let table = new TableLayoutPanel(ColumnCount = 1, RowCount = 2, Dock = DockStyle.Fill, AutoSize = true)
    
    table.RowStyles.Clear()
    table.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)50.0)) |> ignore
    table.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)50.0)) |> ignore
    
    let textBox = new TextBox(Anchor = AnchorStyles.None, AutoSize = false, Width = 100)
    let progressBar = new ProgressBar(Anchor = AnchorStyles.Top)
    
    textBox.TextChanged.Add (onTextChanged textBox progressBar)

    table.Controls.Add textBox
    table.Controls.Add progressBar
    form.Controls.Add table
    
    form

[<EntryPoint>]
let main argv =
    let form = initForm()
    Application.EnableVisualStyles()
    Application.Run(form)
    0