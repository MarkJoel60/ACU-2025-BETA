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
<!--                   SPECIALIZATION OF DECLARED ELEMENTS         -->
<!-- ============================================================= -->

<!ENTITY % learningSummary "learningSummary">
<!ENTITY % learningSummarybody "learningSummarybody">

<!-- declare the structure and content models -->

<!-- declare the class derivations -->

<!ENTITY % learningSummary-info-types "no-topic-nesting">
<!ENTITY included-domains        "" >

<!ENTITY % learningSummary.content
                        "((%title;),
                          (%titlealts;)?,
                          (%shortdesc; | 
                           %abstract;)?,
                          (%prolog;)?,
                          (%learningSummarybody;),
                          (%related-links;)?,
                          (%learningSummary-info-types;)* )"
>
<!ENTITY % learningSummary.attributes
             "id
                        ID 
                                  #REQUIRED
              %conref-atts;
              %select-atts;
              %localization-atts;
              outputclass
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT learningSummary    %learningSummary.content;>
<!ATTLIST learningSummary
              %learningSummary.attributes;
              %arch-atts;
              domains 
                        CDATA
                                  "&included-domains;"
>


<!ENTITY % learningSummarybody.content
                        "(((%lcSummary;) |
                          (%lcObjectives;) |
                          (%lcReview;) |
                          (%lcNextSteps;) |
                          (%lcResources;) |
                          (%section;))*)"
>
<!ENTITY % learningSummarybody.attributes
             "%univ-atts;
              outputclass
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT learningSummarybody     %learningSummarybody.content;>
<!ATTLIST learningSummarybody     %learningSummarybody.attributes;>

<!--specialization attributes-->

<!ATTLIST learningSummary        %global-atts; class CDATA "- topic/topic learningBase/learningBase     learningSummary/learningSummary ">
<!ATTLIST learningSummarybody    %global-atts; class CDATA "- topic/body  learningBase/learningBasebody learningSummary/learningSummarybody ">
