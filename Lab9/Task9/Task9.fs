open System
open System.Drawing
open System.Windows.Forms

let showError message =
    MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore

let getListFromTextBox (txt: TextBox) =
    let str = txt.Text.Trim()
    if String.IsNullOrEmpty str then 
        []
    else
        let parts = str.Split(' ')
        List.ofArray parts |> List.map (fun x -> Int32.Parse x)

let onProcClick (txt:TextBox) (outField: TextBox) eventArgs =
    try
        let list = getListFromTextBox txt
        let allEven = List.fold (fun s x -> if x % 2 = 0 then s else false) true list
        outField.Text <- sprintf "Все четные? %s" (if allEven then "Да!" else "Нет!")
    with
    | :? FormatException -> showError "Неверный формат. Перечислите числа через пробел."
    | e -> showError $"Непредвиденное исключение: {e.Message}"

let buildListInputTable index =
    let table = new TableLayoutPanel(ColumnCount = 2, RowCount = 1, Dock = DockStyle.Fill, AutoSize = false)
    table.ColumnStyles.Clear()
    table.ColumnStyles.Add(new ColumnStyle(sizeType = SizeType.Percent, Width = (float32)20.0)) |> ignore
    table.ColumnStyles.Add(new ColumnStyle(sizeType = SizeType.Percent, Width = (float32)80.0)) |> ignore

    let label = new Label(Text = "Список:", AutoSize = false, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight)
    let textBox = new TextBox(Anchor = AnchorStyles.Left, AutoSize = true, MinimumSize = Size(200, 0))
    table.Controls.Add label
    table.Controls.Add textBox

    table

let initForm() =
    let form = new Form(Width= 400, Height = 300, Text = "F# MainForm")
    let tableRoot = new TableLayoutPanel(ColumnCount = 1, RowCount = 3, Dock = DockStyle.Fill, AutoSize = true)
    
    tableRoot.RowStyles.Clear()
    tableRoot.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)33.3)) |> ignore
    tableRoot.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)33.3)) |> ignore
    tableRoot.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)16.6)) |> ignore

    tableRoot.Controls.Add (buildListInputTable 1)

    let procButton = new Button(Text = "Вычислить", Anchor = AnchorStyles.None, AutoSize = false)
    let resTextBox = new TextBox(Anchor = AnchorStyles.None, AutoSize = false, MinimumSize = Size(200, 0))

    procButton.Click.Add (onProcClick (tableRoot.Controls.[0].Controls.[1] :?> TextBox) resTextBox)

    tableRoot.Controls.Add procButton
    tableRoot.Controls.Add resTextBox
    
    form.Controls.Add tableRoot
    
    form

[<EntryPoint>]
let main argv =
    let form = initForm()
    Application.EnableVisualStyles()
    Application.Run(form)
    0