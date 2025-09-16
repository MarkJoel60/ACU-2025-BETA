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
<!--                    ELEMENT NAME ENTITIES                      -->
<!-- ============================================================= -->

<!ENTITY % anchorref    "anchorref"                                  >
<!ENTITY % keydef       "keydef"                                     >
<!ENTITY % mapref       "mapref"                                     >
<!ENTITY % topicgroup   "topicgroup"                                 >
<!ENTITY % topichead    "topichead"                                  >
<!ENTITY % topicset     "topicset"                                   >
<!ENTITY % topicsetref  "topicsetref"                                >


<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->


<!--                    LONG NAME: Topic Head                      -->
<!ENTITY % topichead.content
                       "((%topicmeta;)?, 
                         (%anchor; | 
                          %data.elements.incl; | 
                          %navref; | 
                          %topicref;)* )"
>
<!ENTITY % topichead.attributes
             "navtitle 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              keys 
                        CDATA 
                                  #IMPLIED
              copy-to 
                        CDATA 
                                  #IMPLIED
              %topicref-atts;
              %univ-atts;"
>
<!ELEMENT topichead    %topichead.content;>
<!ATTLIST topichead    %topichead.attributes;>



<!--                    LONG NAME: Topic Group                     -->
<!ENTITY % topicgroup.content
                       "((%topicmeta;)?, 
                         (%anchor; | 
                          %data.elements.incl; | 
                          %navref; | 
                          %topicref;)* )"
>
<!ENTITY % topicgroup.attributes
             "outputclass 
                        CDATA 
                                  #IMPLIED
              %topicref-atts;
              %univ-atts;"
>
<!ELEMENT topicgroup    %topicgroup.content;>
<!ATTLIST topicgroup    %topicgroup.attributes;>


<!--                    LONG NAME: Anchor Reference                -->
<!ENTITY % anchorref.content
                       "((%topicmeta;)?, 
                         (%data.elements.incl; |
                          %topicref;)* )"
>
<!ENTITY % anchorref.attributes
             "navtitle 
                        CDATA 
                                  #IMPLIED
              href 
                        CDATA 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              keys 
                        CDATA 
                                  #IMPLIED
              query 
                        CDATA 
                                  #IMPLIED
              copy-to 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  #IMPLIED
              type 
                        CDATA 
                                  'anchor'
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              locktitle 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              format 
                        CDATA 
                                  'ditamap'
              linking 
                        (none | 
                         normal | 
                         sourceonly | 
                         targetonly |
                         -dita-use-conref-target) 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              print 
                        (no | 
                         printonly | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              search 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              chunk 
                        CDATA 
                                  'to-navigation'
              %univ-atts;"
>
<!ELEMENT anchorref    %anchorref.content;>
<!ATTLIST anchorref    %anchorref.attributes;>


<!--                    LONG NAME: Map Reference                   -->
<!ENTITY % mapref.content
                       "((%topicmeta;)?, 
                         (%data.elements.incl;)* )"
>
<!ENTITY % mapref.attributes
             "navtitle 
                        CDATA 
                                  #IMPLIED
              href 
                        CDATA 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              keys 
                        CDATA 
                                  #IMPLIED
              query 
                        CDATA 
                                  #IMPLIED
              copy-to 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              format 
                        CDATA 
                                  'ditamap'
              %topicref-atts-without-format;
              %univ-atts;"
>
<!ELEMENT mapref    %mapref.content;>
<!ATTLIST mapref    %mapref.attributes;>


<!--                    LONG NAME: Topicset                        -->
<!ENTITY % topicset.content
                       "((%topicmeta;)?, 
                         (%anchor; | 
                          %data.elements.incl; |
                          %navref; | 
                          %topicref;)* )"
>
<!ENTITY % topicset.attributes
             "navtitle 
                        CDATA 
                                  #IMPLIED
              href 
                        CDATA 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              keys 
                        CDATA 
                                  #IMPLIED
              query 
                        CDATA 
                                  #IMPLIED
              copy-to 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              locktitle 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              linking 
                        (none | 
                         normal | 
                         sourceonly | 
                         targetonly |
                         -dita-use-conref-target) 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              print 
                        (no | 
                         printonly | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              search 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              chunk 
                        CDATA 
                                  'to-navigation'
              id 
                        NMTOKEN 
                                  #REQUIRED
              %conref-atts;
              %select-atts;
              %localization-atts;"
>
<!ELEMENT topicset    %topicset.content;>
<!ATTLIST topicset    %topicset.attributes;>


<!--                    LONG NAME: Topicset Reference              -->
<!ENTITY % topicsetref.content
                       "((%topicmeta;)?, 
                         (%data.elements.incl; |
                          %topicref;)* )"
>
<!ENTITY % topicsetref.attributes
             "navtitle 
                        CDATA 
                                  #IMPLIED
              href 
                        CDATA 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              keys 
                        CDATA 
                                  #IMPLIED
              query 
                        CDATA 
                                  #IMPLIED
              copy-to 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  #IMPLIED
              type 
                        CDATA 
                                  'topicset'
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              locktitle 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              format 
                        CDATA 
                                  'ditamap'
              linking 
                        (none | 
                         normal | 
                         sourceonly | 
                         targetonly |
                         -dita-use-conref-target) 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              print 
                        (no | 
                         printonly | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              search 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              chunk 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT topicsetref    %topicsetref.content;>
<!ATTLIST topicsetref    %topicsetref.attributes;>


<!--                    LONG NAME: Key Definition                  -->
<!ENTITY % keydef.content
                       "((%topicmeta;)?, 
                         (%anchor; | 
                          %data.elements.incl; |
                          %navref; | 
                          %topicref;)* )"
>
<!ENTITY % keydef.attributes
             "navtitle 
                        CDATA 
                                  #IMPLIED
              href 
                        CDATA 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              keys 
                        CDATA 
                                  #REQUIRED
              query 
                        CDATA 
                                  #IMPLIED
              copy-to 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  'resource-only'
              type 
                        CDATA 
                                  #IMPLIED
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              locktitle 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              linking 
                        (none | 
                         normal | 
                         sourceonly | 
                         targetonly |
                         -dita-use-conref-target) 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              print 
                        (no | 
                         printonly | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              search 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              chunk 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT keydef    %keydef.content;>
<!ATTLIST keydef    %keydef.attributes;>


<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->

<!ATTLIST anchorref     %global-atts;  class CDATA "+ map/topicref mapgroup-d/anchorref ">
<!ATTLIST keydef        %global-atts;  class CDATA "+ map/topicref mapgroup-d/keydef ">
<!ATTLIST mapref        %global-atts;  class CDATA "+ map/topicref mapgroup-d/mapref ">
<!ATTLIST topicgroup    %global-atts;  class CDATA "+ map/topicref mapgroup-d/topicgroup ">
<!ATTLIST topichead     %global-atts;  class CDATA "+ map/topicref mapgroup-d/topichead ">
<!ATTLIST topicset      %global-atts;  class CDATA "+ map/topicref mapgroup-d/topicset ">
<!ATTLIST topicsetref   %global-atts;  class CDATA "+ map/topicref mapgroup-d/topicsetref ">


<!-- ================== DITA Map Group Domain  =================== -->