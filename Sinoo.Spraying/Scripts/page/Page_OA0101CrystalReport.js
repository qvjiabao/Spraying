 function ClientDownLoad(){
            $("#HiddenHTML").val(document.documentElement.outerHTML);
            alert( $("#HiddenHTML").va());
            return false;
        }

        function priview() {
            $("#btnPrint").hide();
            $("#btnReturn").hide();
            var bdhtml = window.document.body.innerHTML;
            window.document.body.innerHTML = bdhtml;
            window.print();
            $("#btnPrint").show();
            $("#btnReturn").hide();
        }
                function btnReturns() {
            location.href = "OA0101List.aspx?PageIndex=<%=ViewState["PageIndex"] %> "
        }

        $(function(){
            var sumOB01008=0;
            var sumOB01009=0;
            var sumOB01010=0;
            var sumOB01011=0;
            var sumOB01012=0;
            var sumOB01013=0;
            var sumOB01014=0;
            $.each($(".table_3 tr").slice($("#trproduct").index()+1,$("#trsum").index()),function(){
                sumOB01008+=Number($(this).children().eq(5).text());
                sumOB01009+=Number($(this).children().eq(6).text());
                sumOB01010+=Number($(this).children().eq(7).text());
                sumOB01011+=Number($(this).children().eq(8).text());
                sumOB01012+=Number($(this).children().eq(9).text());
                sumOB01013+=Number($(this).children().eq(10).text());
                sumOB01014+=Number($(this).children().eq(11).text());
            })

            $("#trsum").children().eq(5).text(Number(sumOB01008).toFixed(2));
            $("#trsum").children().eq(6).text(Number(sumOB01009).toFixed(2));
            $("#trsum").children().eq(7).text(Number(sumOB01010).toFixed(2));
            $("#trsum").children().eq(8).text(Number(sumOB01011).toFixed(2));
            $("#trsum").children().eq(9).text(Number(sumOB01012).toFixed(2));
            $("#trsum").children().eq(10).text(Number(sumOB01013).toFixed(2));
            $("#trsum").children().eq(11).text(Number(sumOB01014).toFixed(2));
        })