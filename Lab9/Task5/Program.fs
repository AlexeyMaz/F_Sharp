open System.Windows.Forms

let onButtonClick eventArgs =
    System.Diagnostics.Process.Start("https://fb.ru/misc/i/gallery/38575/3156511.jpg") |> ignore

let onBarChange (trackBar:TrackBar) (button: Button) eventArgs =
    button.Width <- 20 + trackBar.Value * 10

let initForm() =
    let form = new Form(Width= 400, Height = 300, Text = "F# MainForm")
    let table = new TableLayoutPanel(ColumnCount = 1, RowCount = 2, Dock = DockStyle.Fill, AutoSize = true)
    
    table.RowStyles.Clear()
    table.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)50.0)) |> ignore
    table.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)50.0)) |> ignore
    
    let button = new Button(Text = "Маслины", Anchor = AnchorStyles.None, AutoSize = false, Width = 20)
    let trackBar = new TrackBar(Anchor = AnchorStyles.Top)
    button.Click.Add onButtonClick
    trackBar.ValueChanged.Add (onBarChange trackBar button)
    
    table.Controls.Add button
    table.Controls.Add trackBar
    form.Controls.Add table
    
    form

[<EntryPoint>]
let main argv =
    let form = initForm()
    Application.Run(form)
    0