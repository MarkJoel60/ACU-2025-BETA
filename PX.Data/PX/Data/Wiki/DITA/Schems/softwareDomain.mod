<?xml version="1.0" encoding="UTF-8"?>
<!-- ============================================================= -->
<!--                        Acumatica Inc.                         -->
<!--          Copyright (c) 1994-2011 All rights reserved.         -->
<!--                                                               -->
<!--                                                               -->
<!-- This file and its contents are protected by United States     -->
<!-- and International copyright laws.  Unauthorized reproduction  -->
<!-- and/or distribution of all or any portion of the code         -->
<!-- contained here in is strictly prohibited and will result in   -->
<!-- severe civil and criminal penalties.                          -->
<!-- Any violations of this copyright will be prosecuted       	   -->
<!-- to the fullest extent possible under law.                     -->
<!--                                                               -->
<!-- UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE   -->
<!-- OR IN PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES -->
<!-- THE SAME, OR SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY     -->
<!-- ProjectX PRODUCT.                                             -->
<!-- THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.      -->
<!-- ============================================================= -->


<!-- ============================================================= -->
<!--                   ELEMENT NAME ENTITIES                       -->
<!-- ============================================================= -->


<!ENTITY % msgph       "msgph"                                       >
<!ENTITY % msgblock    "msgblock"                                    >
<!ENTITY % msgnum      "msgnum"                                      >
<!ENTITY % cmdname     "cmdname"                                     >
<!ENTITY % varname     "varname"                                     >
<!ENTITY % filepath    "filepath"                                    >
<!ENTITY % userinput   "userinput"                                   >
<!ENTITY % systemoutput 
                       "systemoutput"                                >


<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->


<!--                    LONG NAME: Message Phrase                  -->
<!ENTITY % msgph.content
                       "(%words.cnt;)*"
>
<!ENTITY % msgph.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT msgph    %msgph.content;>
<!ATTLIST msgph    %msgph.attributes;>



<!--                    LONG NAME: Message Block                   -->
<!ENTITY % msgblock.content
                       "(%words.cnt;)*"
>
<!ENTITY % msgblock.attributes
             "%display-atts;
              spectitle 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED 
              xml:space 
                        (preserve) 
                                  #FIXED 'preserve'"
>
<!ELEMENT msgblock    %msgblock.content;>
<!ATTLIST msgblock    %msgblock.attributes;>



<!--                    LONG NAME: Message Number                  -->
<!ENTITY % msgnum.content
                       "(#PCDATA |
                         %text;)*"
>
<!ENTITY % msgnum.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT msgnum    %msgnum.content;>
<!ATTLIST msgnum    %msgnum.attributes;>



<!--                    LONG NAME: Command Name                    -->
<!ENTITY % cmdname.content
                       "(#PCDATA |
                         %text;)*"
>
<!ENTITY % cmdname.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT cmdname    %cmdname.content;>
<!ATTLIST cmdname    %cmdname.attributes;>



<!--                    LONG NAME: Variable Name                   -->
<!ENTITY % varname.content
                       "(#PCDATA |
                         %text;)*"
>
<!ENTITY % varname.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT varname    %varname.content;>
<!ATTLIST varname    %varname.attributes;>


<!--                    LONG NAME: File Path                       -->
<!ENTITY % filepath.content
                       "(%words.cnt;)*"
>
<!ENTITY % filepath.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT filepath    %filepath.content;>
<!ATTLIST filepath    %filepath.attributes;>



<!--                    LONG NAME: User Input                      -->
<!ENTITY % userinput.content
                       "(%words.cnt;)*"
>
<!ENTITY % userinput.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT userinput    %userinput.content;>
<!ATTLIST userinput    %userinput.attributes;>



<!--                    LONG NAME: System Output                   -->
<!ENTITY % systemoutput.content
                       "(%words.cnt;)*"
>
<!ENTITY % systemoutput.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT systemoutput    %systemoutput.content;>
<!ATTLIST systemoutput    %systemoutput.attributes;>

 

<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->
 

<!ATTLIST cmdname     %global-atts;  class CDATA "+ topic/keyword sw-d/cmdname ">
<!ATTLIST filepath    %global-atts;  class CDATA "+ topic/ph sw-d/filepath "    >
<!ATTLIST msgblock    %global-atts;  class CDATA "+ topic/pre sw-d/msgblock "   >
<!ATTLIST msgnum      %global-atts;  class CDATA "+ topic/keyword sw-d/msgnum " >
<!ATTLIST msgph       %global-atts;  class CDATA "+ topic/ph sw-d/msgph "       >
<!ATTLIST systemoutput
                      %global-atts;  class CDATA "+ topic/ph sw-d/systemoutput ">
<!ATTLIST userinput   %global-atts;  class CDATA "+ topic/ph sw-d/userinput "   >
<!ATTLIST varname     %global-atts;  class CDATA "+ topic/keyword sw-d/varname ">

 
<!-- ================== End Software Domain ====================== -->