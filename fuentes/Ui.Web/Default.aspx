<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Protozoo.Ui.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="/js/jquery-1.7.2.min.js" ></script>
    <script language="javascript" type="text/javascript" src="/js/jquery-ui-1.8.22.custom.min.js" ></script>
    <style>
        body 
        {
            font-family: Arial; 
            background-image: url('img/bg.jpeg');
            text-align: justify;
            line-height: 1.5em;
        }
              
	
        input[type="text"]:focus {
		-webkit-box-shadow: 0px 0px 5px #007eff;
		-moz-box-shadow: 0px 0px 5px #007eff;
		box-shadow: 0px 0px 5px #007eff;
	}
        
        input[type="text"] 
        {
            /*
            border: 1px solid rgba(0,0,0,0.5);
           border-radius: 4px;    
           box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -o-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -webkit-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -moz-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           padding: 0.2em;
           padding: 5px;
           */
	        font-size: 15px;
	        text-shadow: 0px 1px 0px #fff;
	        outline: none;
	        background: -webkit-gradient(linear, left top, left bottom, from(#bcbcbe), to(#ffffff));
	        background: -moz-linear-gradient(top,  #bcbcbe,  #ffffff);
	        -webkit-border-radius: 3px;
	        -moz-border-radius: 3px;
	        border-radius: 3px;
	        border: 1px solid #717171;
	        -webkit-box-shadow: 1px 1px 0px #efefef;
	        -moz-box-shadow: 1px 1px 0px #efefef;
	        box-shadow:  1px 1px 0px #efefef;
        }
        
        .card
        {
            color: #222;
            text-shadow: 0px 2px 3px #555;                     
           margin: 10px;
           padding: 2em;
           border: 1px solid rgba(0,0,0,0.5);
           border-radius: 10px 10px 2px 2px;
           background: rgba(100,100,100,0.25);
           box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -o-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -webkit-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -moz-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
          /* background: transparent url('img/user02-50x50.png') no-repeat 0.75em 1.5em;*/
           
        }
        
        input[type="submit"]
        {
            color: #222;
            text-shadow: 0px 2px 3px #555;                     
           margin: 10px;
           padding: 0.7em;
           border: 1px solid rgba(0,0,0,0.5);
           border-radius: 3px 7px 3px 7px;
           background: rgba(200,200,200,0.15);
           box-shadow: 
                0 2px 6px rgba(0,0,0,0.5), 
                inset 0 1px rgba(255,255,255,0.3), 
                inset 0 10px rgba(255,255,255,0.2), 
                inset 0 10px 20px rgba(255,255,255,0.25), 
                inset 0 -15px 30px rgba(0,0,0,0.3);
          /*
           -o-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -webkit-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           -moz-box-shadow: 0 2px 6px rgba(0,0,0,0.5), inset 0 1px rgba(255,255,255,0.3), inset 0 10px rgba(255,255,255,0.2), inset 0 10px 20px rgba(255,255,255,0.25), inset 0 -15px 30px rgba(0,0,0,0.3);
           */
          /* background: transparent url('img/user02-50x50.png') no-repeat 0.75em 1.5em;*/
           
        }
        
        input[type="submit"]:focus 
        {            
            box-shadow: 
                0px 0px 5px rgba(0,126,255,1),
                inset 0 1px rgba(255,255,255,0.3), 
                inset 0 10px rgba(255,255,255,0.2), 
                inset 0 10px 20px rgba(255,255,255,0.25), 
                inset 0 -15px 30px rgba(0,0,0,0.3);
            
        }
        
        .card > label 
        {
            display: inline-block;
            margin-bottom: 0.3em;
            font-weight: normal;
            width: 7em;
            margin-left: 3em;
        }
        
        .card-title { font-weight: bold; border-bottom: 1px solid #222; margin-bottom: 1.7em;}
        
        #userCard   
        {
            width: 30%;
            background:  url('img/user02-50x50.png') no-repeat 0.75em 1.5em;
        }
        #request-result-box 
        {
            width: 40%;
        }
        
    </style>
    <script language="javascript" type="text/javascript">
        $(function() {
            $('#userCard').draggable();
            $('#request-result-box').draggable();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sc01"></asp:ScriptManager>
    <div>
        <asp:TextBox ID="ctlDoContext" runat="server" Text="1" />        
        <asp:Button id="ctlDo" runat="server" Text="Ejecutar operación"/>
        
        <div class="card" id="request-result-box"> 
            <div class="card-title">Resultado de la operación</div>
            <div ID="ctlRes" runat="server" ></div>
            <asp:Repeater ID="msgList" runat="server" >
                <ItemTemplate>
                   <div><%#DataBinder.GetDataItem(Container) %></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="card"id="userCard">        
            <label>Nombre</label>
            <asp:Label ID="ctlUserName" runat="server" />
            <br />
            <label>Cuenta</label>
            <asp:Label ID="ctlUserAccount" runat="server" />    
        </div>
    </div>
    </form>
</body>
</html>
