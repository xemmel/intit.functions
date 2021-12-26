<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:template match="Input">
        <Output>
            <Id>
                <xsl:value-of select="id" />
            </Id>
            <Name>
                <xsl:text>Clara</xsl:text>
            </Name>
        </Output>
    </xsl:template>
</xsl:stylesheet>