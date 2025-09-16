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

<!ENTITY % index-see       "index-see"                               >
<!ENTITY % index-see-also  "index-see-also"                          >
<!ENTITY % index-sort-as   "index-sort-as"                           >


<!-- ============================================================= -->
<!--                    COMMON ATTLIST SETS                        -->
<!-- ============================================================= -->




<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS for IMAGEMAP          -->
<!-- ============================================================= -->

<!--                    LONG NAME: Index See                       -->
<!ENTITY % index-see.content
                       "(%words.cnt; |
                         %indexterm;)*"
>
<!ENTITY % index-see.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT index-see    %index-see.content;>
<!ATTLIST index-see    %index-see.attributes;>


<!--                    LONG NAME: Index See Also                  -->
<!ENTITY % index-see-also.content
                       "(%words.cnt; |
                         %indexterm;)*"
>
<!ENTITY % index-see-also.attributes
             "keyref 
                            CDATA 
                                            #IMPLIED
              %univ-atts;"
>
<!ELEMENT index-see-also    %index-see-also.content;>
<!ATTLIST index-see-also    %index-see-also.attributes;>


<!--                    LONG NAME: Index Sort As                   -->
<!ENTITY % index-sort-as.content
                       "(%words.cnt;)*"
>
<!ENTITY % index-sort-as.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT index-sort-as    %index-sort-as.content;>
<!ATTLIST index-sort-as    %index-sort-as.attributes;>


<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->


<!ATTLIST index-see       %global-atts; class CDATA "+ topic/index-base indexing-d/index-see ">
<!ATTLIST index-see-also  %global-atts; class CDATA "+ topic/index-base indexing-d/index-see-also ">
<!ATTLIST index-sort-as   %global-atts; class CDATA "+ topic/index-base indexing-d/index-sort-as ">
 
<!-- ================== End Indexing Domain ====================== -->