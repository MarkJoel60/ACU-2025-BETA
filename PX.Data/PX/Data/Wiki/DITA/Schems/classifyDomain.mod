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

<!ENTITY % topicsubject        "topicsubject">
<!ENTITY % topicapply          "topicapply"><!--New-->
<!ENTITY % subjectref          "subjectref">
<!ENTITY % topicSubjectTable   "topicSubjectTable">
<!ENTITY % topicSubjectHeader  "topicSubjectHeader">
<!ENTITY % topicSubjectRow     "topicSubjectRow">
<!ENTITY % topicCell           "topicCell">
<!ENTITY % subjectCell         "subjectCell">

<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->

<!--                    LONG NAME: Topic Subject                   -->
<!-- SKOS equivalent:  primary if href or keyref are specified     -->
<!ENTITY % topicsubject.content
                       "((%topicmeta;)?,
                         (%data.elements.incl; |
                          %subjectref;|
                          %topicref;)*)"
>
<!ENTITY % topicsubject.attributes
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
              type 
                        CDATA 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  'resource-only'
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  'no'
              %univ-atts;"
>
<!ELEMENT topicsubject    %topicsubject.content;>
<!ATTLIST topicsubject    %topicsubject.attributes;>


<!--                    LONG NAME: Topic Apply                     -->
<!ENTITY % topicapply.content
                       "((%topicmeta;)?,
                         (%data.elements.incl; |
                          %subjectref; |
                          %topicref;)*)"
>
<!ENTITY % topicapply.attributes
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
              collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  'resource-only'
              scope 
                        (external | 
                         local | 
                         peer | 
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
                                  'no'
              %univ-atts;"
>
<!ELEMENT topicapply    %topicapply.content;>
<!ATTLIST topicapply    %topicapply.attributes;>


<!--                    LONG NAME: Subject Reference               -->
<!ENTITY % subjectref.content
                       "((%topicmeta;)?,
                         (%data.elements.incl;)*)"
>
<!ENTITY % subjectref.attributes
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
              collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  'resource-only'
              scope 
                        (external | 
                         local | 
                         peer | 
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
                                  'no'
              %univ-atts;"
>
<!ELEMENT subjectref    %subjectref.content;>
<!ATTLIST subjectref    %subjectref.attributes;>


<!--                   LONG NAME: Topic Subject Relationship Table -->
<!ENTITY % topicSubjectTable.content
                       "((%title;),
                         (%topicmeta;)?,
                         (%topicSubjectHeader;)?,
                         (%topicSubjectRow;)+)"
>
<!ENTITY % topicSubjectTable.attributes
             "%topicref-atts-no-toc;
              %univ-atts;"
>
<!ELEMENT topicSubjectTable    %topicSubjectTable.content;>
<!ATTLIST topicSubjectTable    %topicSubjectTable.attributes;>


<!--                    LONG NAME: Topic Subject Table Header      -->
<!-- The header defines the set of subjects for each column.
     By default, the subject in the header cell must be a broader ancestor
         within a scheme available during processing for the subjects
         in the same column of other rows
     In the header, the topicCell serves primarily as a placeholder
         for the topic column but could also provide some constraints
         or metadata for the topics -->
<!ENTITY % topicSubjectHeader.content
                       "((%topicCell;),
                         (%subjectCell;)+)"
>
<!ENTITY % topicSubjectHeader.attributes
             "%univ-atts;"
>
<!ELEMENT topicSubjectHeader    %topicSubjectHeader.content;>
<!ATTLIST topicSubjectHeader    %topicSubjectHeader.attributes;>


<!--                    LONG NAME: Topic Subject Table Row         -->
<!ENTITY % topicSubjectRow.content
                       "((%topicCell;),
                         (%subjectCell;)+)"
>
<!ENTITY % topicSubjectRow.attributes
             "%univ-atts;"
>
<!ELEMENT topicSubjectRow    %topicSubjectRow.content;>
<!ATTLIST topicSubjectRow    %topicSubjectRow.attributes;>


<!--                    LONG NAME: Topic Subject Table Cell        -->
<!ENTITY % topicCell.content
                       "((%data.elements.incl; |
                          %topicref;)+)"
>
<!ENTITY % topicCell.attributes
             "%univ-atts;
              %topicref-atts;"
>
<!ELEMENT topicCell    %topicCell.content;>
<!ATTLIST topicCell    %topicCell.attributes;>


<!--                    LONG NAME: Topic Subject Cell              -->
<!ENTITY % subjectCell.content
                       "((%data.elements.incl; |
                          %subjectref; |
                          %topicref;)*)"
>
<!ENTITY % subjectCell.attributes
             "%univ-atts;
              %topicref-atts;"
>
<!ELEMENT subjectCell    %subjectCell.content;>
<!ATTLIST subjectCell    %subjectCell.attributes;>


<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->

<!ATTLIST topicsubject %global-atts;
    class CDATA "+ map/topicref classify-d/topicsubject ">
<!ATTLIST topicapply %global-atts;
    class CDATA "+ map/topicref classify-d/topicapply ">
<!ATTLIST subjectref %global-atts;
    class CDATA "+ map/topicref classify-d/subjectref ">
<!ATTLIST topicSubjectTable %global-atts;
    class CDATA "+ map/reltable classify-d/topicSubjectTable ">
<!ATTLIST topicSubjectHeader %global-atts;
    class CDATA "+ map/relrow classify-d/topicSubjectHeader ">
<!ATTLIST topicSubjectRow %global-atts;
    class CDATA "+ map/relrow classify-d/topicSubjectRow ">
<!ATTLIST topicCell %global-atts;
    class CDATA "+ map/relcell classify-d/topicCell ">
<!ATTLIST subjectCell %global-atts;
    class CDATA "+ map/relcell classify-d/subjectCell ">

<!-- ================== DITA Subject Classification Domain  ====== -->
