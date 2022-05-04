open System
open System.Drawing
open System.Windows.Forms

let union arr1 arr2 =
    Array.filter (fun x -> not (Array.contains x arr1)) arr2 |> Array.append arr1 |> Array.sort

let showError message =
    MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore

let getArrayFromTextBox (txt: TextBox) =
    let str = txt.Text.Trim()
    if String.IsNullOrEmpty str then 
        [||]
    else
        let parts = str.Split(' ')
        Array.map (fun str -> Int32.Parse str) parts

let onProcClick (txt1:TextBox) (txt2:TextBox) (outField: TextBox) eventArgs =
    try
        let arr1 = getArrayFromTextBox txt1
        let arr2 = getArrayFromTextBox txt2
        let united = union arr1 arr2
        outField.Text <- (String.Join (" ", united))
    with
    | :? FormatException -> showError "Неверный формат. Перечислите числа через пробел."
    | e -> showError $"Непредвиденное исключение: {e.Message}"

let buildArrayInputTable index =
    let table = new TableLayoutPanel(ColumnCount = 2, RowCount = 1, Dock = DockStyle.Fill, AutoSize = false)
    table.ColumnStyles.Clear()
    table.ColumnStyles.Add(new ColumnStyle(sizeType = SizeType.Percent, Width = (float32)20.0)) |> ignore
    table.ColumnStyles.Add(new ColumnStyle(sizeType = SizeType.Percent, Width = (float32)80.0)) |> ignore

    let label = new Label(Text = $"Массив {index}:", AutoSize = false, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight)
    let textBox = new TextBox(Anchor = AnchorStyles.Left, AutoSize = true, MinimumSize = Size(200, 0))
    table.Controls.Add label
    table.Controls.Add textBox

    table

let initForm() =
    let form = new Form(Width= 400, Height = 300, Text = "F# MainForm")
    let tableRoot = new TableLayoutPanel(ColumnCount = 1, RowCount = 4, Dock = DockStyle.Fill, AutoSize = true)
    
    tableRoot.RowStyles.Clear()
    tableRoot.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)33.3)) |> ignore
    tableRoot.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)33.3)) |> ignore
    tableRoot.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)16.6)) |> ignore
    tableRoot.RowStyles.Add(new RowStyle(sizeType = SizeType.Percent, Height = (float32)16.6)) |> ignore

    tableRoot.Controls.Add (buildArrayInputTable 1)
    tableRoot.Controls.Add (buildArrayInputTable 2)

    let procButton = new Button(Text = "Объединить", Anchor = AnchorStyles.None, AutoSize = false, MinimumSize = Size(100, 0))
    let resTextBox = new TextBox(Anchor = AnchorStyles.None, AutoSize = false, MinimumSize = Size(200, 0))

    procButton.Click.Add (onProcClick (tableRoot.Controls.[0].Controls.[1] :?> TextBox) (tableRoot.Controls.[1].Controls.[1] :?> TextBox) resTextBox)

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