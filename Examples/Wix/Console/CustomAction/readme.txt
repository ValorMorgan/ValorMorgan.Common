Steps

1) Copy the contents of the CopyThisToWixProj folder into your WixProj and make the license file "Content".
1.2) Delete the CopyThisToWixProj folder.
1.3) delete the Product.wxs file, if that came with this nuget package.
2) Open up Product.tt and update the values that are relevant for this project.
3) Verify that the project file for your CustomAction project has the following xml:

    <Content Include="Product.tt">
      <Generator>TextTemplatingFileGenerator</Generator>
      <LastGenOutput>Product.wxs</LastGenOutput>
    </Content>

	Note -- I've had to update the LastGenOutput every time so that it isn't pointing to Proudct1.wxs.
4) Generate the product.wxs from the T4 template.
5) look at the bottom of the generated product.wsx file.   There are some comments for updating build actions and settings.   Comply with them.
6) Update the References of your WiX project.   This is typically done by including a reference to both your Web project and your CustomActions project (this project)
7) Try (and fail) to build the wix project.
8) search your wxs folder for the updated file id and paste it into the installerInfo.CustomActionFileKey value in the related line of code in product.tt
9) Update the name of the MSI in the WiX project.
10) Rebuild.  If you did everything right, you should have a functioning MSI.
