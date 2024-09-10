$(document).ready(function () {
    function createFormField() {
        return `
                        <hr/>
                <div class="form-element-wrapper field">
                        <button type="button" class="btn btn-danger ml-2 remove-field">Kaldır</button>
                    <div class="form-group d-flex align-items-center ">
                        <input type="text" class="form-control mr-2 m-1" name="fields[]" placeholder="Parametre Adı">
                        <select class="form-control mr-2" name="fields[]" m-1">
                            <option value="" disabled selected>Veri Tipi</option>
                            <option value="string">String</option>
                            <option value="int">Integer</option>
                            <option value="bool">Boolean</option>
                            <option value="date">Date</option>
                        </select>
                        <div class="form-check form-check-inline">
                            <input type="checkbox" class="form-check-input" name="fields[]">
                            <label class="form-check-label">Zorunlu</label>
                        </div>
                    </div>
                 </div>
                `;
    }

    $('#form-builder').click(function () {
        $('#myModal').modal('show');
    });
    $(document).on('click', '.remove-field', function () {
        $(this).parent($(".form-element-wrapper")).remove();
    });
    $('#addParamButton').click(function () {
        var newFormField = createFormField();
        $('#modalForm').append(newFormField);
    });
    $("#modalForm").on("submit", function (event) {
        event.preventDefault(); 
        var fields = [];
        $(".form-element-wrapper.field").each(function () {
            var name = $(this).find("input[type='text']").val();
            var dataType = $(this).find("select").val();
            var isRequired = $(this).find("input[type='checkbox']").is(":checked");
            fields.push({
                Name: name,
                DataType: dataType,
                IsRequired: isRequired
            });
        });
        $("<input>").attr({
            type: "hidden",
            name: "fieldsJson",
            value: JSON.stringify(fields)
        }).appendTo("#modalForm");
        this.submit();
    });
});