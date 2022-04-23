cd src
dotnet lambda deploy-function SendMail 
aws lambda update-function-configuration --function-name SendMail --environment "Variables={PROVIDER=provider,USERNAME=username,PASSWORD=password,EMAIL=email,PORT=port}"
@pause