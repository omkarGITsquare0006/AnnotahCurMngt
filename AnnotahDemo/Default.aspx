<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AnnotahDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container-fluid">
        <div class="card">
            <div class="card-header font-weight-bold">
                Sample Heading
            </div>
            <div class="card-body">
                <div class="form-inline input-group">
                    <div class="form-inline input-group">
                        <label for="po" class="mr-sm-2">PO Number:</label>
                        <input type="text" class="form-control" id="po">
                        <div class="input-group-append">
                            <button class="btn btn-secondary" onclick="GetData();" type="button" data-toggle="modal" data-target="#myModal">
                                <i class="fa fa-search"></i>
                            </button>
                        </div>
                    </div>
                </div>
                <div class="row mt-3">
                    <div class="col">
                        <input type="checkbox" class="custom-checkbox" id="chkOne" name="chkOne">
                        <label class="form-check-label" for="chkOne">Check1</label>
                    </div>
                    <div class="col">
                        <input type="checkbox" class="custom-checkbox" id="chkTwo" name="chkTwo">
                        <label class="form-check-label" for="chkTwo">Check2</label>
                    </div>
                    <div class="col">
                        <input type="checkbox" class="custom-checkbox" id="chkThree" name="chkThree">
                        <label class="form-check-label" for="chkThree">Check3</label>
                    </div>
                </div>
                <div class="container rounded mt-4 border border-dark table-responsive">
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">First</th>
                                <th scope="col">Last</th>
                                <th scope="col">Handle</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">1</th>
                                <td>Mark</td>
                                <td>Otto</td>
                                <td>@mdo</td>
                            </tr>
                            <tr>
                                <th scope="row">2</th>
                                <td>Jacob</td>
                                <td>Thornton</td>
                                <td>@fat</td>
                            </tr>
                            <tr>
                                <th scope="row">3</th>
                                <td>Larry</td>
                                <td>the Bird</td>
                                <td>@twitter</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <!-- The Modal -->
    <div class="modal" id="myModal">
        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h6 class="modal-title">Select PO Number</h6>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body  table-responsive">
                    <table class="table table-sm" id="example">
                        <thead>
                            <tr class="">
                                <th scope='col'>PO #</th>
                                <th scope='col'>Date</th>
                                <th scope='col'>POHFCY</th>
                                <th scope='col'>Select PO</th>
                            </tr>
                        </thead>
                        <tbody id="oTable">
                        </tbody>
                    </table>
                </div>

                <!-- Modal footer -->
                <%--<div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>--%>
            </div>
        </div>
    </div>

    <script>
        function GetData() {
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetData_Modal",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (jqXHR, sStatus, sErrorThrown) {
                    alert('data:  ' + sErrorThrown);
                    alert('Get Data Error:  ' + sStatus);
                },
                success: function (data) {
                    $("#oTable").empty();
                    var oTable = data.d;
                    //alert(JSON.stringify(data));
                    for (i = 0; i <= oTable.Rows.length - 1; i++) {
                        var milli = new Date(parseInt(oTable.Rows[i].ORDDAT_0.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        var d = milli.format('dd-MM-yyyy hh:mm:ss');
                        //alert(d);
                        $("#oTable").append(
                            "<tr><td class='po" + i + "'>" + oTable.Rows[i].POHNUM_0 + "</td><td>" + d + "</td><td>" + oTable.Rows[i].POHFCY_0 + "</td>" +
                            "<td><input type='button' id='id' class='btn btn-success btn-sm' value='select' rownr='" + i + "'></td></tr>");
                    }
                }
            });
        }

        $(document).on("click", "#id", function () {
            //alert("PO Number: " + $('.po' + $(this).attr('rownr') + '').text());
            var ponumber = $('.po' + $(this).attr('rownr') + '').text();
            $('#myModal').modal('hide');
            $("#po").val(ponumber);
        });

    </script>
</asp:Content>
