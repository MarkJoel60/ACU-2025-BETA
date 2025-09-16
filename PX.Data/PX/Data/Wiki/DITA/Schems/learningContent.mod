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

<!-- ============ Specialization of declared elements ============ -->

<!ENTITY % learningContent "learningContent">
<!ENTITY % learningContentbody "learningContentbody">

<!-- declare the structure and content models -->

<!-- declare the class derivations -->

<!ENTITY % learningContent-info-types "no-topic-nesting">
<!ENTITY included-domains    "" >

<!ENTITY % learningContent.content
                       "((%title;),
                         (%titlealts;)?,
                         (%shortdesc; | 
                          %abstract;)?,
                         (%prolog;)?,
                         (%learningContentbody;),
                         (%related-links;)?,
                         (%learningContent-info-types;)* )"
>
<!ENTITY % learningContent.attributes
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
<!ELEMENT learningContent    %learningContent.content;>
<!ATTLIST learningContent
              %learningContent.attributes;
              %arch-atts;
              domains 
                        CDATA
                                  "&included-domains;"
>


<!ENTITY % learningContentbody.content
                       "(((%lcIntro;) |
                          (%lcDuration;) |
                          (%lcObjectives;))*,
                         (%lcChallenge;)?,
                         (%lcInstruction;)?,
                         (%section;)*)  "
>
<!ENTITY % learningContentbody.attributes
             "%univ-atts;
              outputclass
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT learningContentbody    %learningContentbody.content;>
<!ATTLIST learningContentbody    %learningContentbody.attributes;>



<!--specialization attributes-->

<!ATTLIST learningContent        %global-atts; class CDATA "- topic/topic learningBase/learningBase learningContent/learningContent ">
<!ATTLIST learningContentbody    %global-atts; class CDATA "- topic/body  learningBase/learningBasebody learningContent/learningContentbody ">
